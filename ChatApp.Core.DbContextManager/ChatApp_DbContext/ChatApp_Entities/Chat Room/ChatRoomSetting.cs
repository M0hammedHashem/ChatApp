
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{

    public class ChatRoomSettings
    {
        [Key]
        [ForeignKey("ChatRoom")]
        public int ChatRoomId { get; set; }
        public bool MembersCanSendMessage { get; set; } = true; // Default to true

        public bool MembersCanSendAttachments { get; set; } = true; // Default to true

        public virtual ChatRoom ChatRoom { get; set; }
    }
}
