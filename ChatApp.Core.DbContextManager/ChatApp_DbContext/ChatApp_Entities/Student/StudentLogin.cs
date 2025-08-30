using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("Student_Login")]
    public class StudentLogin
    {
        [Key] // Marks LoginID as the primary key
        [Column("LoginID")]
        public int LoginId { get; set; }
        [Column("StudentID")]
        public string StudentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        public Student Student { get; set; }
        public SchoolBranch School { get; set; }

    }
}
