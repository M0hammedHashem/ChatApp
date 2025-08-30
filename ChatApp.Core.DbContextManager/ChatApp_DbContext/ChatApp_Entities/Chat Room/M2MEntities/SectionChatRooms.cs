namespace ChatApp.Core.DbContextManager
{

    public class SectionChatRooms
    {
        public int SectionId { get; set; }
        public int ChatRoomId { get; set; }

        public Section Section { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }

}
