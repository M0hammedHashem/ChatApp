using ChatApp.Core.DbContextManager;

namespace ChatApp.Core.IDataService
{
    public class ChatRoomMembers_DTO
    {

        public int ChatRoomUserId { get; set; }
        public int ChatRoomId { get; set; }
        public ChatRoomUserType ChatRoomUserType { get; set; }
        public ChatRoom_DTO? ChatRoom { get; set; }
        public ChatRoomUser_DTO? ChatRoomUser { get; set; }
    }
}
