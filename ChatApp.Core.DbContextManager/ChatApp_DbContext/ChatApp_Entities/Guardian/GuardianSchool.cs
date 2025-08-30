using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    public class GuardianSchool
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("GuardianID")]
        public int GuardianId { get; set; }
        [Column("SchoolID")]
        public int SchoolId { get; set; }
        public Guardian Guardian { get; set; }
        public SchoolBranch School { get; set; }

    }
}
