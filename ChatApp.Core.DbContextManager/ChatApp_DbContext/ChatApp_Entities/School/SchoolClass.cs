using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("SchoolClasses")]
    public class SchoolClass
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SchoolClassID")]
        public int SchoolClassId { get; set; }

        [Column("ClassID")]
        public int ClassId { get; set; }

        [Column("SchoolID")]
        public int SchoolId { get; set; }

        [Column("CurriculumID")]
        public int CurriculumId { get; set; }

        [MaxLength(255)]
        public string SchoolClassEnglishName { get; set; }
        [MaxLength(255)]
        public string SchoolClassArabicName { get; set; }

        #region Table Relations.

        public ICollection<StudentSchoolDetails> Students { get; set; } = new List<StudentSchoolDetails>();
        public ICollection<Section> Sections { get; set; } = new List<Section>();

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<ClassChatRooms> ChatRooms { get; set; } = new List<ClassChatRooms>();

        public SchoolBranch School { get; set; } = new SchoolBranch();


        #endregion
    }
}
