namespace ChatApp.Core.DbContextManager
{

    public class ChatRoomMembers
    {

        public int ChatRoomUserId { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoomUserType ChatRoomUserType { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public ChatRoomUser ChatRoomUser { get; set; }
    }
}
