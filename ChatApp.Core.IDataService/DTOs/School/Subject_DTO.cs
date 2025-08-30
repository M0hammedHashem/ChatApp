namespace ChatApp.Core.IDataService
{
    public class Subject_DTO
    {
        #region Public Properties

        public int SubjectId { get; set; }
        public int SchoolClassId { get; set; }
        public int SchoolId { get; set; }
        public int SectionId { get; set; }
        public string SubjectArabicName { get; set; }
        public string SubjectEnglishName { get; set; }


        #endregion Public Properties

        #region Table Relations.

        public SchoolClass_DTO SchoolClass { get; set; } = new SchoolClass_DTO();
        public Section_DTO Section { get; set; } = new Section_DTO();
        public SchoolBranch_DTO School { get; set; } = new SchoolBranch_DTO();
        public ICollection<SubjectChatRooms_DTO> ChatRooms { get; set; } = new List<SubjectChatRooms_DTO>();



        #endregion Table Relations.
    }
}