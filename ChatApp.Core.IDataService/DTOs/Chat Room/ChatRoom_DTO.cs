using ChatApp.Core.DbContextManager;

namespace ChatApp.Core.IDataService
{
    public class ChatRoom_DTO
    {

        public int ChatRoomId { get; set; }
        public string ArabicChatRoomName { get; set; }
        public string EnglishChatRoomName { get; set; }
        public int SchoolId { get; set; }
        public SchoolBranch_DTO School { get; set; }
        public ChatRoomType ChatRoomType { get; set; }
        public int? LastMessageId { get; set; }

        public ChatRoomMessage_DTO? LastMessage { get; set; }

        public ICollection<Staff_DTO> Staffs { get; set; } = new List<Staff_DTO>();
        public ICollection<Section_DTO> Sections { get; set; } = new List<Section_DTO>();
        public ICollection<Subject_DTO> Subjects { get; set; } = new List<Subject_DTO>();
        public ICollection<SchoolClass_DTO> SchoolClasses { get; set; } = new List<SchoolClass_DTO>();
        public ICollection<ChatRoomMessage_DTO> Messages { get; set; } = new List<ChatRoomMessage_DTO>();
        public ICollection<ChatRoomMembers_DTO> Members { get; set; }// Navigation property for members


        public ICollection<Curriculum_DTO> Curriculums { get; set; } = new List<Curriculum_DTO>();
    }
}
