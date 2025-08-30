namespace ChatApp.Core.IDataService
{

    public class CurriculumChatRooms_DTO
    {
        public int CurriculumId { get; set; }
        public int ChatRoomId { get; set; }

        public Curriculum_DTO Curriculum { get; set; }
        public ChatRoom_DTO ChatRoom { get; set; }
    }

}
