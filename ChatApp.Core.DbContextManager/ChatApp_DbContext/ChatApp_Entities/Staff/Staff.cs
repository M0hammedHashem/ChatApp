using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{

    [Table("Staff")]
    public class Staff
    {
        [Key]
        [Column("StaffID")]

        public string? StaffId { get; set; }

        public string? StaffArabicName { get; set; }

        public string? StaffEnglishName { get; set; }



    }
}
