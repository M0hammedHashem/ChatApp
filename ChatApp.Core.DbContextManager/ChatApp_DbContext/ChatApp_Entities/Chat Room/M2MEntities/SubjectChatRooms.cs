namespace ChatApp.Core.DbContextManager
{

    public class SubjectChatRooms
    {
        public int SubjectId { get; set; }
        public int ChatRoomId { get; set; }

        public Subject Subject { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }

}
