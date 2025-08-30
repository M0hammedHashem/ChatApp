namespace ChatApp.Core.IDataService
{

    public class CurriculumDepartment_DTO
    {

        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public int CurriculumId { get; set; }

        public int SchoolId { get; set; }

        public Department_DTO Department { get; set; }
        public Curriculum_DTO Curriculum { get; set; }
        public SchoolBranch_DTO School { get; set; }
    }
}
