namespace ChatApp.Core.IDataService
{
    public class StudentSchoolDetails_DTO
    {
        public string StudentId { get; set; }
        public int SchoolId { get; set; }
        public int ClassId { get; set; }
        public int? SectionId { get; set; }

        #region Table Relations.

        public Student_DTO Student { get; set; }
        public Section_DTO Section { get; set; }
        public SchoolBranch_DTO SchoolBranch { get; set; }
        public SchoolClass_DTO Class { get; set; }

        #endregion

    }
}
