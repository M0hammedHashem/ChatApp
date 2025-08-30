using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("CurriculumDepartment")]
    public class CurriculumDepartment
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("DepartmentID")]
        public int DepartmentId { get; set; }
        [Column("CurriculumID")]
        public int CurriculumId { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        public Department Department { get; set; }
        public Curriculum Curriculum { get; set; }
        public SchoolBranch School { get; set; }
    }
}
