using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("Subjects")]
    public class Subject
    {
        #region Public Properties

        [Column("SubjectID")]
        public int SubjectId { get; set; }

        [Column("SchoolClassID")]
        public int SchoolClassId { get; set; }

        [Column("SchoolID")]
        public int SchoolId { get; set; }

        [Column("SectionID")]
        public int SectionId { get; set; }

        [MaxLength(255)]
        public string SubjectArabicName { get; set; }

        [MaxLength(255)]
        public string SubjectEnglishName { get; set; }

        #endregion Public Properties

        #region Table Relations.

        public SchoolClass SchoolClass { get; set; }
        public Section Section { get; set; }
        public SchoolBranch School { get; set; }
        public ICollection<SubjectChatRooms> ChatRooms { get; set; } = new List<SubjectChatRooms>();

        #endregion Table Relations.
    }
}