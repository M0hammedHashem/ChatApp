using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;
using ChatApp.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ChatApp.Web.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IChatAppDataServiceFactory _ds;
        private readonly IUnitOfWork _unitOfWork;

        public ChatController(IChatAppDataServiceFactory ds, IUnitOfWork unitOfWork)
        {
            _ds = ds;
            _unitOfWork = unitOfWork;
        }

        private string Username => User.FindFirstValue(ClaimTypes.NameIdentifier);
        private string Password => User.FindFirstValue("Password");
        private string Role => User.FindFirstValue(ClaimTypes.Role);
        private bool IsSuperAdmin => !User.HasClaim(c => c.Type == "SchoolId");
        private int? GetAdminSchoolId()
        {
            var schoolIdClaim = User.FindFirst("SchoolId");
            if (schoolIdClaim != null && int.TryParse(schoolIdClaim.Value, out int schoolId))
            {
                return schoolId;
            }
            return null;
        }


        public async Task<IActionResult> Index(int? roomId)
        {
            var username = this.Username;
            if (string.IsNullOrEmpty(username)) return Challenge();

            string userId = await GetUserId();
            var model = new ChatViewModel();
            var chatRoomUser = (await _ds.CreateChatRoomUserService.Where(u => u.UserId == userId)).FirstOrDefault();
            if (chatRoomUser == null) return View(model);

            var memberships = await _ds.CreateChatRoomMembersService.Where(m => m.ChatRoomUserId == chatRoomUser.ChatRoomUserId);
            var roomIds = memberships.Select(m => m.ChatRoomId).ToList();
            if (!roomIds.Any()) return View(model);

            var allRooms = await _ds.CreateChatRoomService.GetAllAsync();
            var userRooms = allRooms.Where(r => roomIds.Contains(r.ChatRoomId)).ToList();

            var messages = (await _ds.CreateChatRoomMessageService.Where(m => m.ChatRoomId != 0, m => m.FromUser)).ToList();
            var allMessagesForUserRooms = messages.Where(m => roomIds.Contains(m.ChatRoomId)).ToList();

            var lastMessagesLookup = allMessagesForUserRooms
                .GroupBy(m => m.ChatRoomId)
                .ToDictionary(g => g.Key, g => g.OrderByDescending(m => m.Timestamp).FirstOrDefault());

            foreach (var room in userRooms)
            {
                if (lastMessagesLookup.TryGetValue(room.ChatRoomId, out var lastMessage))
                {
                    room.LastMessage = lastMessage;
                }
            }

            model.Rooms = userRooms
                .OrderByDescending(r => r.LastMessage?.Timestamp ?? DateTime.MinValue)
                .ToList();

            var activeRoomId = roomId ?? model.Rooms.FirstOrDefault()?.ChatRoomId;
            if (activeRoomId.HasValue && roomIds.Contains(activeRoomId.Value))
            {
                model.ActiveRoom = model.Rooms.FirstOrDefault(r => r.ChatRoomId == activeRoomId);

                model.Messages = allMessagesForUserRooms
                    .Where(m => m.ChatRoomId == activeRoomId.Value)
                    .OrderBy(m => m.Timestamp)
                    .ToList();

                model.ActiveRoomSettings = await _ds.CreateChatRoomSettingsService.FindAsync(activeRoomId.Value)
                                                  ?? new ChatRoomSettings_DTO();

                var activeRoomMembership = memberships.FirstOrDefault(m => m.ChatRoomId == activeRoomId.Value);
                var schoolAdminId = GetAdminSchoolId();
                bool isSchoolAdminForThisRoom = schoolAdminId.HasValue && model.ActiveRoom.SchoolId == schoolAdminId.Value;

                if (IsSuperAdmin || isSchoolAdminForThisRoom || activeRoomMembership?.ChatRoomUserType == ChatRoomUserType.Admin)
                {
                    model.IsCurrentUserRoomAdmin = true;
                }
            }

            var filterTypes = new List<SelectListItem> { new SelectListItem("All Rooms", "") };
            var roomTypes = Enum.GetValues(typeof(ChatRoomType)).Cast<ChatRoomType>();
            foreach (var type in roomTypes)
            {
                var text = System.Text.RegularExpressions.Regex.Replace(type.ToString(), "(\\B[A-Z])", " $1");
                filterTypes.Add(new SelectListItem($"{text} Rooms", type.ToString()));
            }
            model.FilterTypes = filterTypes;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreMessages(int roomId, long lastMessageTimestampTicks)
        {
            var username = this.Username;
            if (string.IsNullOrEmpty(username)) return Unauthorized();

            var chatRoomUser = (await _ds.CreateChatRoomUserService.Where(u => u.UserName == username)).FirstOrDefault();
            if (chatRoomUser == null) return Unauthorized();

            var isMember = (await _ds.CreateChatRoomMembersService.Where(m => m.ChatRoomUserId == chatRoomUser.ChatRoomUserId && m.ChatRoomId == roomId)).Any();
            if (!isMember) return Forbid();

            var lastTimestamp = new DateTime(lastMessageTimestampTicks, DateTimeKind.Utc);

            var olderMessages = (await _ds.CreateChatRoomMessageService.Where(
                    m => m.ChatRoomId == roomId && m.Timestamp < lastTimestamp,
                    m => m.FromUser))
                .OrderByDescending(m => m.Timestamp)
                .Take(50)
                .OrderBy(m => m.Timestamp)
                .ToList();

            return Json(olderMessages);
        }

        private async Task<string> GetUserId()
        {
            if (Role == "Student")
            {
                var student = (await _ds.CreateStudentLoginService.Where(s => s.Username == Username && s.Password == Password)).FirstOrDefault();
                return student?.StudentId ?? "None";
            }
            else if (Role == "Guardian")
            {
                var guardian = (await _ds.CreateGuardianLoginService.Where(g => g.UserName == Username && g.Password == Password)).FirstOrDefault();
                return guardian?.GuardianId ?? "None";
            }
            else if (Role == "Admin")
            {
                return "";
            }
            else
            {
                var staff = (await _ds.CreateStaffLoginService.Where(s => s.UserName == Username && s.Password == Password)).FirstOrDefault();
                return staff?.StaffId ?? "None";
            }
        }
    }
}

