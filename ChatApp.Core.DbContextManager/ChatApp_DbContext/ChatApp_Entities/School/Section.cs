using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("SectionID")]
        public int SectionId { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        [Column("SchoolClassID")]
        public int SchoolClassId { get; set; }
        [MaxLength(255)]
        public string SectionArabicName { get; set; }
        [MaxLength(255)]
        public string SectionEnglishName { get; set; }
        public string SectionCode { get; set; }


        #region Table Relations.

        public SchoolClass SchoolClass { get; set; }
        public SchoolBranch School { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<SectionChatRooms> ChatRooms { get; set; } = new List<SectionChatRooms>();
        public ICollection<StudentSchoolDetails> Students { get; set; } = new List<StudentSchoolDetails>();




        #endregion
    }
}
