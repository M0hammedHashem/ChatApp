namespace ChatApp.Core.IDataService
{

    public class SubjectChatRooms_DTO
    {
        public int SubjectId { get; set; }
        public int ChatRoomId { get; set; }

        public Subject_DTO Subject { get; set; }
        public ChatRoom_DTO ChatRoom { get; set; }
    }

}
