using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("StudentSchoolDetails")]
    public class StudentSchoolDetails
    {
        [Key, ForeignKey("Student")]
        [MaxLength(255), Column("StudentID")]
        public string StudentId { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        [Column("ClassID")]
        public int ClassId { get; set; }

        [Column("SectionID")]
        public int? SectionId { get; set; }

        #region Table Relations.

        public Student Student { get; set; }
        public Section Section { get; set; }
        public SchoolBranch School { get; set; }
        public SchoolClass Class { get; set; }

        #endregion

    }
}
