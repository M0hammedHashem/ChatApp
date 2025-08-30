namespace ChatApp.Core.IDataService
{
    public class SchoolBranch_DTO
    {
        public int SchoolId { get; set; }
        public string? SchoolArabicName { get; set; }
        public string? SchoolEnglishName { get; set; }

        #region Table Relations.

        public ICollection<SchoolClass_DTO> SchoolClasses { get; set; } = new List<SchoolClass_DTO>();
        public ICollection<Section_DTO> Sections { get; set; } = new List<Section_DTO>();
        public ICollection<Subject_DTO> Subjects { get; set; } = new List<Subject_DTO>();
        public ICollection<ChatRoom_DTO> ChatRooms { get; set; } = new List<ChatRoom_DTO>();
        public ICollection<StudentSchoolDetails_DTO> Students { get; set; } = new List<StudentSchoolDetails_DTO>();




        #endregion
    }
}
