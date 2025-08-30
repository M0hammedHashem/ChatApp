using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    [Table("StaffJobDetails")]
    public class StaffJobDetails
    {
        [Key, ForeignKey("Staff")]
        [Column("StaffID")]
        public string StaffId { get; set; }
        [Column("SchoolID")]
        public int? SchoolId { get; set; }
        public int? Department { get; set; }
        public Staff Staff { get; set; }
        public SchoolBranch School { get; set; }
    }
}
