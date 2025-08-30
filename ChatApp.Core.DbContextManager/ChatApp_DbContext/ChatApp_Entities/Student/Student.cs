using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [MaxLength(255)]
        [Column("StudentID")]
        public string? StudentId { get; set; }

        [MaxLength(255)]
        public string? StudentArabicName { get; set; }

        [MaxLength(255)]
        public string? StudentEnglishName { get; set; }
        [Column("GuardianID")]
        public int? GuardianId { get; set; }


        #region Table Relations.

        public Guardian Guardian { get; set; }

        #endregion

    }
}
