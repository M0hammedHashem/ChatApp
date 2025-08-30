using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("Department")]
    public class Department
    {
        [Key]
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        public bool IsAcademic { get; set; }
        public string DepartmentArabicName { get; set; }
        public string DepartmentEnglishName { get; set; }
        public ICollection<CurriculumDepartment> Curriculums { get; set; } = new List<CurriculumDepartment>();

        public SchoolBranch School { get; set; }


    }
}