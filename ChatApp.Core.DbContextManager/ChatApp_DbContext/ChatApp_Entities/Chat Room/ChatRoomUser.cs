namespace ChatApp.Core.DbContextManager
{

    public class ChatRoomUser
    {
        public int ChatRoomUserId { get; set; }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }

        public ChatterType UserType { get; set; }
        public ICollection<ChatRoomMembers> ChatRooms { get; set; } = new List<ChatRoomMembers>();
    }
}
