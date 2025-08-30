namespace ChatApp.Core.DbContextManager
{

    public class CurriculumChatRooms
    {
        public int CurriculumId { get; set; }
        public int ChatRoomId { get; set; }

        public Curriculum Curriculum { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }

}
