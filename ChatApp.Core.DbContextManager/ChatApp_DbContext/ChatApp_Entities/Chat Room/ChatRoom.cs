using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{

    public class ChatRoom
    {
        public int ChatRoomId { get; set; }
        [Required]
        public string ArabicChatRoomName { get; set; }
        [Required]
        public string EnglishChatRoomName { get; set; }
        public int? LastMessageId { get; set; }
        [ForeignKey("LastMessageId")]
        public ChatRoomMessage? LastMessage { get; set; }

        public int SchoolId { get; set; }
        public ChatRoomType ChatRoomType { get; set; }

        #region Relationships
        public SchoolBranch School { get; set; }

        public ICollection<ChatRoomMessage> Messages { get; set; } = new List<ChatRoomMessage>();
        public ICollection<ChatRoomMembers> Members { get; set; }

        public ICollection<CurriculumChatRooms> Curriculums { get; set; } = new List<CurriculumChatRooms>();
        public ICollection<SectionChatRooms> Sections { get; set; } = new List<SectionChatRooms>();
        public ICollection<SubjectChatRooms> Subjects { get; set; } = new List<SubjectChatRooms>();
        public ICollection<ClassChatRooms> SchoolClasses { get; set; } = new List<ClassChatRooms>();

        #endregion

    }

}
