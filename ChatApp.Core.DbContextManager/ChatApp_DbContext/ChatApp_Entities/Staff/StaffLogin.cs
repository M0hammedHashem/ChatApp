using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("Users")]
    public class StaffLogin
    {
        [Key]
        [Column("UserID")]
        public int UserId { get; set; }
        [Column("StaffID")]
        public string StaffId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        [Column("UserType")]
        public StaffType StaffType { get; set; }
        public SchoolBranch School { get; set; }
        public Staff Staff { get; set; }



    }
}