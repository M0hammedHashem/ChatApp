using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{

    [Table("TeacherExperiences")]
    public class Teacher
    {
        [Key]
        [Column("TeacherExperienceId")]
        public int TeacherExperienceId { get; set; }
        [Column("TeacherID"), ForeignKey("Staff")]
        public string TeacherId { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        [Column("Curriculum")]
        public int CurriculumId { get; set; }
        [Column("SchoolClassID")]
        public int SchoolClassId { get; set; }
        [Column("SubjectID")]
        public int SubjectId { get; set; }
        public string SchoolYear { get; set; }
        [ForeignKey("TeacherId")]
        public Staff Staff { get; set; }

    }
}
