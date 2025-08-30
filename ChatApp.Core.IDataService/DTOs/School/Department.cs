namespace ChatApp.Core.IDataService
{
    public class Department_DTO
    {

        public int DepartmentId { get; set; }
        public int SchoolId { get; set; }
        public bool IsAcademic { get; set; }
        public string DepartmentArabicName { get; set; }
        public string DepartmentEnglishName { get; set; }
        public ICollection<CurriculumDepartment_DTO> Curriculums { get; set; } = new List<CurriculumDepartment_DTO>();
        public SchoolBranch_DTO School_DTO { get; set; }


    }
}