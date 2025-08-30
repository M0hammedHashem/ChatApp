namespace ChatApp.Core.DbContextManager
{

    public class ClassChatRooms
    {
        public int SchoolClassId { get; set; }
        public int ChatRoomId { get; set; }

        public SchoolClass SchoolClass { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }

}
