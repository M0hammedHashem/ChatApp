namespace ChatApp.Core.IDataService
{
    public class Section_DTO
    {
        public int SectionId { get; set; }
        public int SchoolId { get; set; }
        public int SchoolClassId { get; set; }
        public string SectionCode { get; set; }

        public string SectionArabicName { get; set; }

        public string SectionEnglishName { get; set; }


        #region Table Relations.
        public SchoolClass_DTO SchoolClass { get; set; }
        public SchoolBranch_DTO School { get; set; }
        public ICollection<Subject_DTO> Subjects { get; set; } = new List<Subject_DTO>();
        public ICollection<SectionChatRooms_DTO> ChatRomms { get; set; } = new List<SectionChatRooms_DTO>();
        public ICollection<StudentSchoolDetails_DTO> Students { get; set; } = new List<StudentSchoolDetails_DTO>();

        #endregion
    }
}
