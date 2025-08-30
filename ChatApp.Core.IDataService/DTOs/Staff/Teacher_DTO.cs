namespace ChatApp.Core.IDataService
{
    public class Teacher_DTO
    {

        public int TeacherExperienceId { get; set; }
        public string TeacherId { get; set; }
        public int SchoolId { get; set; }
        public int CurriculumId { get; set; }
        public int SchoolClassId { get; set; }
        public int SubjectId { get; set; }
        public string SchoolYear { get; set; }
        public Staff_DTO Staff { get; set; }

    }
}
