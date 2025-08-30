using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("SchoolBranches")]
    public class SchoolBranch
    {
        [Key]
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        public string? SchoolArabicName { get; set; }
        public string? SchoolEnglishName { get; set; }

        #region Table Relations.

        public ICollection<SchoolClass> SchoolClasses { get; set; } = new List<SchoolClass>();
        public ICollection<Section> Sections { get; set; } = new List<Section>();
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<ChatRoom> ChatRooms { get; set; } = new List<ChatRoom>();
        public ICollection<StudentSchoolDetails> Students { get; set; } = new List<StudentSchoolDetails>();


        #endregion
    }
}
