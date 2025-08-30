using ChatApp.Core.DbContextManager;

namespace ChatApp.Core.IDataService
{
    public class ChatRoomUser_DTO
    {

        public int ChatRoomUserId { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public ChatterType UserType { get; set; }
        public ICollection<ChatRoomMembers_DTO> ChatRooms { get; set; } = new List<ChatRoomMembers_DTO>();
    }

}
