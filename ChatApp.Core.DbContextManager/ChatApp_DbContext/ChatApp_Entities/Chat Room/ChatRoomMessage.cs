
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Core.DbContextManager
{

    public class ChatRoomMessage
    {

        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public System.DateTime Timestamp { get; set; }
        [Required]
        public int FromUserId { get; set; }
        public ChatRoomUser FromUser { get; set; }
        [Required]
        public int ChatRoomId { get; set; }
        [Required]
        // This is the one-to-many relationship
        public ChatRoom ChatRoom { get; set; }

        // This is the one-to-one relationship
        public ChatRoom? LastMessageChatRoom { get; set; } // The inverse navigation property
        // The type of message (Text, Image, or File)
        public MessageType MessageType { get; set; }

        // URL to the uploaded file (for Images and Files)
        public string? AttachmentUrl { get; set; }

        // The original name of the uploaded file
        public string? AttachmentFileName { get; set; }
    }

}
