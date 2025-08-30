using ChatApp.Core.IDataService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChatApp.Web.ViewModels
{
    /// A comprehensive ViewModel for the main chat page.
    /// It holds all the data needed to display the room list, active chat window, and filters.
    /// </summary>
    public class ChatViewModel
    {
        public List<ChatRoom_DTO> Rooms { get; set; }
        public ChatRoom_DTO ActiveRoom { get; set; }
        public List<ChatRoomMessage_DTO> Messages { get; set; }
        public IEnumerable<SelectListItem> FilterTypes { get; set; }
        public bool IsCurrentUserRoomAdmin { get; set; }

        // --- NEW PROPERTY ---
        /// <summary>
        /// The settings for the currently active chat room.
        /// </summary>
        public ChatRoomSettings_DTO ActiveRoomSettings { get; set; }

        public ChatViewModel()
        {
            Rooms = new List<ChatRoom_DTO>();
            Messages = new List<ChatRoomMessage_DTO>();
            FilterTypes = new List<SelectListItem>();
            IsCurrentUserRoomAdmin = false;
            // Initialize with default settings to prevent null errors
            ActiveRoomSettings = new ChatRoomSettings_DTO { MembersCanSendMessage = true, MembersCanSendAttachments = true };
        }
    }
}
