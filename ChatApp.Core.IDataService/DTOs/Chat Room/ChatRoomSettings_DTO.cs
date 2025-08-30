namespace ChatApp.Core.IDataService
{
    public class ChatRoomSettings_DTO
    {

        public int ChatRoomId { get; set; }
        public bool MembersCanSendMessage { get; set; } = true; // Default to true
        public bool MembersCanSendAttachments { get; set; } = true; // Default to true
        public virtual ChatRoom_DTO ChatRoom { get; set; }
    }
}
