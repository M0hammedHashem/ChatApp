using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.Web.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly IChatAppDataServiceFactory _ds;
        private readonly IUnitOfWork _unitOfWork;

        public ChatHub(IChatAppDataServiceFactory ds, IUnitOfWork unitOfWork)
        {
            _ds = ds;
            _unitOfWork = unitOfWork;
        }

        public override async Task OnConnectedAsync()
        {
            var username = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(username))
            {
                var chatRoomUser = (await _ds.CreateChatRoomUserService.Where(u => u.UserName == username)).FirstOrDefault();
                if (chatRoomUser != null)
                {
                    var memberships = await _ds.CreateChatRoomMembersService.Where(m => m.ChatRoomUserId == chatRoomUser.ChatRoomUserId);
                    foreach (var membership in memberships)
                    {
                        await Groups.AddToGroupAsync(Context.ConnectionId, membership.ChatRoomId.ToString());
                    }
                }
            }
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(int roomId, string messageContent)
        {
            if (string.IsNullOrWhiteSpace(messageContent)) return;

            var fromUser = await GetCurrentUser();
            var membership = await GetUserMembership(fromUser?.ChatRoomUserId, roomId);
            if (fromUser == null || membership == null) return;

            var settings = await GetRoomSettings(roomId);
            if (!settings.MembersCanSendMessage && membership.ChatRoomUserType == ChatRoomUserType.Member)
            {
                await Clients.Caller.SendAsync("ReceiveError", "This room is in announcement-only mode.");
                return;
            }

            var message = new ChatRoomMessage_DTO
            {
                ChatRoomId = roomId,
                Content = messageContent,
                FromUserId = fromUser.ChatRoomUserId,
                Timestamp = DateTime.UtcNow,
                MessageType = MessageType.Text
            };

            var savedMessageWithId = await _ds.CreateChatRoomMessageService.AddAsync(message);
            if (savedMessageWithId == null) return;

            await _unitOfWork.SaveChangesAsync(); // Commit the new message

            var finalMessage = (await _ds.CreateChatRoomMessageService.Where(m => m.Id == savedMessageWithId.Id, m => m.FromUser)).FirstOrDefault();
            if (finalMessage == null) return;

            var payload = CreateMessagePayload(finalMessage, finalMessage.FromUser);
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", payload);
        }

        [HubMethodName("SendAttachment")]
        public async Task SendAttachmentMessage(int roomId, string attachmentUrl, string fileName)
        {
            if (string.IsNullOrWhiteSpace(attachmentUrl) || string.IsNullOrWhiteSpace(fileName)) return;

            var fromUser = await GetCurrentUser();
            var membership = await GetUserMembership(fromUser?.ChatRoomUserId, roomId);
            if (fromUser == null || membership == null) return;

            var settings = await GetRoomSettings(roomId);
            if (!settings.MembersCanSendAttachments && membership.ChatRoomUserType == ChatRoomUserType.Member)
            {
                await Clients.Caller.SendAsync("ReceiveError", "Attachments are disabled in this room.");
                return;
            }

            var message = new ChatRoomMessage_DTO
            {
                ChatRoomId = roomId,
                FromUserId = fromUser.ChatRoomUserId,
                Timestamp = DateTime.UtcNow,
                MessageType = GetMessageTypeFromFileName(fileName),
                AttachmentUrl = attachmentUrl,
                AttachmentFileName = fileName,
                Content = ""
            };

            var savedMessageWithId = await _ds.CreateChatRoomMessageService.AddAsync(message);
            if (savedMessageWithId == null) return;

            await _unitOfWork.SaveChangesAsync(); // Commit the new message

            var finalMessage = (await _ds.CreateChatRoomMessageService.Where(m => m.Id == savedMessageWithId.Id, m => m.FromUser)).FirstOrDefault();
            if (finalMessage == null) return;

            var payload = CreateMessagePayload(finalMessage, finalMessage.FromUser);
            await Clients.Group(roomId.ToString()).SendAsync("ReceiveMessage", payload);
        }

        public async Task DeleteMessage(int messageId)
        {
            var currentUser = await GetCurrentUser();
            if (currentUser == null) return;

            var messageToDelete = await _ds.CreateChatRoomMessageService.FindAsync(messageId);
            if (messageToDelete == null) return;

            var membership = await GetUserMembership(currentUser.ChatRoomUserId, messageToDelete.ChatRoomId);
            bool isRoomAdmin = membership?.ChatRoomUserType == ChatRoomUserType.Admin;

            if (messageToDelete.FromUserId == currentUser.ChatRoomUserId || isRoomAdmin)
            {
                await _ds.CreateChatRoomMessageService.DeleteAsync(messageToDelete);
                await _unitOfWork.SaveChangesAsync();
                await Clients.Group(messageToDelete.ChatRoomId.ToString()).SendAsync("MessageDeleted", messageId);
            }
        }

        public async Task EditMessage(int messageId, string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent)) return;

            var currentUser = await GetCurrentUser();
            if (currentUser == null) return;

            var messageToEdit = await _ds.CreateChatRoomMessageService.FindAsync(messageId);

            if (messageToEdit != null && messageToEdit.FromUserId == currentUser.ChatRoomUserId)
            {
                messageToEdit.Content = newContent;
                // **THE FIX:** We don't need to call UpdateAsync. 
                // The DbContext is already tracking the messageToEdit object for changes.
                // await _ds.CreateChatRoomMessageService.UpdateAsync(messageToEdit);

                // We just need to save the changes that were made to the tracked object.
                await _unitOfWork.SaveChangesAsync();

                await Clients.Group(messageToEdit.ChatRoomId.ToString()).SendAsync("MessageEdited", messageId, newContent);
            }
        }

        public async Task UserIsTyping(int roomId)
        {
            var user = await GetCurrentUser();
            if (user == null) return;
            await Clients.OthersInGroup(roomId.ToString()).SendAsync("UserTyping", user.Name, roomId);
        }

        public async Task UserStoppedTyping(int roomId)
        {
            var user = await GetCurrentUser();
            if (user == null) return;
            await Clients.OthersInGroup(roomId.ToString()).SendAsync("UserStoppedTyping", user.Name, roomId);
        }

        #region Helper Methods

        private async Task<ChatRoomUser_DTO> GetCurrentUser()
        {
            var chatRoomUserId = await GetChatRoomUserId();
            if (chatRoomUserId == 0) return null;
            return await _ds.CreateChatRoomUserService.FindAsync(chatRoomUserId);
        }

        private async Task<int> GetChatRoomUserId()
        {
            var username = Context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var password = Context.User.FindFirstValue("Password");
            var role = Context.User.FindFirstValue(ClaimTypes.Role);

            if (role == "Student")
            {
                var student = (await _ds.CreateStudentLoginService.Where(s => s.Username == username && s.Password == password)).FirstOrDefault();
                if (student == null) return 0;
                var chatRoomUser = (await _ds.CreateChatRoomUserService.Where(c => c.UserId == student.StudentId && c.UserType == ChatterType.Student)).FirstOrDefault();
                return chatRoomUser?.ChatRoomUserId ?? 0;
            }
            else if (role == "Guardian")
            {
                var guardian = (await _ds.CreateGuardianLoginService.Where(g => g.UserName == username && g.Password == password)).FirstOrDefault();
                if (guardian == null) return 0;
                var chatRoomUser = (await _ds.CreateChatRoomUserService.Where(c => c.UserId == guardian.GuardianId && c.UserType == ChatterType.Guardian)).FirstOrDefault();
                return chatRoomUser?.ChatRoomUserId ?? 0;
            }
            else if (role == "Admin")
            {
                return 0; // Admins don't send messages as themselves
            }
            else // Staff or Teacher
            {
                var staff = (await _ds.CreateStaffLoginService.Where(s => s.UserName == username && s.Password == password)).FirstOrDefault();
                if (staff == null) return 0;
                var chatRoomUser = (await _ds.CreateChatRoomUserService.Where(c => c.UserId == staff.StaffId)).FirstOrDefault();
                return chatRoomUser?.ChatRoomUserId ?? 0;
            }
        }

        private async Task<ChatRoomMembers_DTO> GetUserMembership(int? chatRoomUserId, int roomId)
        {
            if (chatRoomUserId == null) return null;
            var memberships = await _ds.CreateChatRoomMembersService.Where(m => m.ChatRoomId == roomId && m.ChatRoomUserId == chatRoomUserId.Value);
            return memberships.FirstOrDefault();
        }

        private async Task<ChatRoomSettings_DTO> GetRoomSettings(int roomId)
        {
            return await _ds.CreateChatRoomSettingsService.FindAsync(roomId) ?? new ChatRoomSettings_DTO();
        }

        private MessageType GetMessageTypeFromFileName(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return imageExtensions.Contains(extension) ? MessageType.Image : MessageType.File;
        }

        private object CreateMessagePayload(ChatRoomMessage_DTO message, ChatRoomUser_DTO fromUser)
        {
            return new
            {
                messageId = message.Id,
                content = message.Content,
                fromUsername = fromUser.UserName,
                fromName = fromUser.Name,
                fromUserType = fromUser.UserType.ToString(),
                timestamp = message.Timestamp,
                messageType = message.MessageType.ToString(),
                attachmentUrl = message.AttachmentUrl,
                attachmentFileName = message.AttachmentFileName,
                roomId = message.ChatRoomId
            };
        }

        #endregion
    }
}

