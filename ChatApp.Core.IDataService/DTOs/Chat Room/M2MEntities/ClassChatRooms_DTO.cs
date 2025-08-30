namespace ChatApp.Core.IDataService
{

    public class ClassChatRooms_DTO
    {
        public int SchoolClassId { get; set; }
        public int ChatRoomId { get; set; }

        public SchoolClass_DTO SchoolClass { get; set; }
        public ChatRoom_DTO ChatRoom { get; set; }
    }

}
