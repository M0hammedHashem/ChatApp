using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("WebUsers")]
    public class GuardianLogin
    {
        [Key]
        public int WebUserId { get; set; }
        [Column("UserSystemID")]
        public string? GuardianId { get; set; }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        public SchoolBranch School { get; set; }


    }
}
