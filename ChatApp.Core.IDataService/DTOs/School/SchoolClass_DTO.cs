namespace ChatApp.Core.IDataService
{
    public class SchoolClass_DTO
    {

        public int SchoolClassId { get; set; }

        public int ClassId { get; set; }

        public int SchoolId { get; set; }

        public int CurriculumId { get; set; }

        public string SchoolClassEnglishName { get; set; }
        public string SchoolClassArabicName { get; set; }

        #region Table Relations.

        public ICollection<StudentSchoolDetails_DTO> Students { get; set; } = new List<StudentSchoolDetails_DTO>();
        public ICollection<Section_DTO> Sections { get; set; } = new List<Section_DTO>();

        public ICollection<Subject_DTO> Subjects { get; set; } = new List<Subject_DTO>();
        public ICollection<ClassChatRooms_DTO> ChatRooms { get; set; } = new List<ClassChatRooms_DTO>();

        public SchoolBranch_DTO School { get; set; } = new SchoolBranch_DTO();


        #endregion
    }
}
