namespace ChatApp.Core.IDataService
{

    public class SectionChatRooms_DTO
    {
        public int SectionId { get; set; }
        public int ChatRoomId { get; set; }

        public Section_DTO Section { get; set; }
        public ChatRoom_DTO ChatRoom { get; set; }
    }

}
