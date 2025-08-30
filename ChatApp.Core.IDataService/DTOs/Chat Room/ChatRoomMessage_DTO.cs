using ChatApp.Core.DbContextManager;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.IDataService
{
    public class ChatRoomMessage_DTO
    {

        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public System.DateTime Timestamp { get; set; }
        [Required]
        public int FromUserId { get; set; }
        public ChatRoomUser_DTO FromUser { get; set; }
        [Required]
        public int ChatRoomId { get; set; }
        public ChatRoom_DTO ChatRoom { get; set; }
        public ChatRoom_DTO? LastMessageChatRoom { get; set; }



        // The type of message (Text, Image, or File)
        public MessageType MessageType { get; set; }

        // URL to the uploaded file (for Images and Files)
        public string? AttachmentUrl { get; set; }

        // The original name of the uploaded file
        public string? AttachmentFileName { get; set; }

    }
}
