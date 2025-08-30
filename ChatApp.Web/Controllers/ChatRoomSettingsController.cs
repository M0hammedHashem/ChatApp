using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;
using ChatApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ChatRoomSettingsController : Controller
    {
        private readonly IChatAppDataServiceFactory _ds;
        private readonly IUnitOfWork _unitOfWork;

        public ChatRoomSettingsController(IChatAppDataServiceFactory ds, IUnitOfWork unitOfWork)
        {
            _ds = ds;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int roomId)
        {
            try
            {
                var room = await _ds.CreateChatRoomService.FindAsync(roomId);
                if (room == null) return NotFound();

                var settings = await _ds.CreateChatRoomSettingsService.FindAsync(roomId) ?? new ChatRoomSettings_DTO { ChatRoomId = roomId };

                // --- Robustly fetch staff for the dropdown ---
                var allStaffLoginsInSchool = (await _ds.CreateStaffLoginService.GetAllAsync()).Where(s => s.SchoolId == room.SchoolId).ToList();
                var staffIds = allStaffLoginsInSchool.Select(s => s.StaffId).ToHashSet();
                var allStaffDetails = await _ds.CreateStaffService.GetAllAsync();
                var staffDetailsInSchool = allStaffDetails.Where(s => staffIds.Contains(s.StaffId)).ToList();
                var staffDetailsLookup = staffDetailsInSchool.ToDictionary(s => s.StaffId);

                var allStaffForDropdown = allStaffLoginsInSchool
                    .Where(sl => staffDetailsLookup.ContainsKey(sl.StaffId))
                    .Select(sl => new SelectListItem(
                        staffDetailsLookup[sl.StaffId].StaffEnglishName,
                        sl.UserName
                    )).ToList();

                // --- Robustly fetch current admins ---
                var roomAdmins = await _ds.CreateChatRoomMembersService.Where(m => m.ChatRoomId == roomId && m.ChatRoomUserType == ChatRoomUserType.Admin);
                var roomAdminUserIds = roomAdmins.Select(ra => ra.ChatRoomUserId).ToHashSet();
                var allChatUsers = await _ds.CreateChatRoomUserService.GetAllAsync();
                var adminUsernames = allChatUsers
                    .Where(u => roomAdminUserIds.Contains(u.ChatRoomUserId))
                    .Select(u => u.UserName)
                    .ToList();

                var model = new ChatRoomSettingsViewModel
                {
                    ChatRoomId = room.ChatRoomId,
                    ChatRoomName = room.EnglishChatRoomName,
                    MembersCanSendMessage = settings.MembersCanSendMessage,
                    MembersCanSendAttachments = settings.MembersCanSendAttachments,
                    AllStaff = allStaffForDropdown,
                    SelectedAdminUsernames = adminUsernames
                };

                return View(model);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred loading the settings page: {ex.Message}";
                return RedirectToAction("Index", "Chat");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ChatRoomSettingsViewModel model)
        {
            if (model.SelectedAdminUsernames == null)
            {
                model.SelectedAdminUsernames = new List<string>();
            }

            if (!ModelState.IsValid)
            {
                // Re-populate dropdown if validation fails
                var room = await _ds.CreateChatRoomService.FindAsync(model.ChatRoomId);
                var allStaffInSchool = await _ds.CreateStaffLoginService.Where(s => s.SchoolId == room.SchoolId, s => s.Staff);
                model.AllStaff = allStaffInSchool.Select(s => new SelectListItem(s.Staff.StaffEnglishName, s.UserName));
                return View(model);
            }

            try
            {
                // 1. Update basic settings
                var settingsService = _ds.CreateChatRoomSettingsService;
                var settings = await settingsService.FindAsync(model.ChatRoomId);

                if (settings == null)
                {
                    await settingsService.AddAsync(new ChatRoomSettings_DTO
                    {
                        ChatRoomId = model.ChatRoomId,
                        MembersCanSendMessage = model.MembersCanSendMessage,
                        MembersCanSendAttachments = model.MembersCanSendAttachments
                    });
                }
                else
                {
                    settings.MembersCanSendMessage = model.MembersCanSendMessage;
                    settings.MembersCanSendAttachments = model.MembersCanSendAttachments;
                    await settingsService.UpdateAsync(settings);
                }

                // 2. Synchronize Room Admins
                var membersService = _ds.CreateChatRoomMembersService;
                var allChatUsers = await _ds.CreateChatRoomUserService.GetAllAsync();
                var allMembersInRoom = await membersService.Where(m => m.ChatRoomId == model.ChatRoomId);

                var selectedAdminUsers = allChatUsers.Where(u => model.SelectedAdminUsernames.Contains(u.UserName)).ToList();
                var selectedAdminUserIds = selectedAdminUsers.Select(u => u.ChatRoomUserId).ToHashSet();

                // Demote admins who are no longer selected
                var adminsToDemote = allMembersInRoom.Where(m => m.ChatRoomUserType == ChatRoomUserType.Admin && !selectedAdminUserIds.Contains(m.ChatRoomUserId)).ToList();
                foreach (var member in adminsToDemote)
                {
                    member.ChatRoomUserType = ChatRoomUserType.Member;
                    await membersService.UpdateAsync(member);
                }

                // Promote or add new admins
                foreach (var userToMakeAdmin in selectedAdminUsers)
                {
                    var existingMembership = allMembersInRoom.FirstOrDefault(m => m.ChatRoomUserId == userToMakeAdmin.ChatRoomUserId);

                    if (existingMembership != null)
                    {
                        if (existingMembership.ChatRoomUserType == ChatRoomUserType.Member)
                        {
                            existingMembership.ChatRoomUserType = ChatRoomUserType.Admin;
                            await membersService.UpdateAsync(existingMembership);
                        }
                    }
                    else
                    {
                        await membersService.AddAsync(new ChatRoomMembers_DTO
                        {
                            ChatRoomId = model.ChatRoomId,
                            ChatRoomUserId = userToMakeAdmin.ChatRoomUserId,
                            ChatRoomUserType = ChatRoomUserType.Admin
                        });
                    }
                }

                // Commit all changes in one transaction
                await _unitOfWork.SaveChangesAsync();

                TempData["SuccessMessage"] = "Chat room settings saved successfully!";
                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while saving settings: {ex.Message}";
                var room = await _ds.CreateChatRoomService.FindAsync(model.ChatRoomId);
                var allStaffInSchool = await _ds.CreateStaffLoginService.Where(s => s.SchoolId == room.SchoolId, s => s.Staff);
                model.AllStaff = allStaffInSchool.Select(s => new SelectListItem(s.Staff.StaffEnglishName, s.UserName));
                return View(model);
            }
        }
    }
}

