using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{

    public class Guardian
    {
        [Column("GuardianID")]
        public int GuardianId { get; set; }

        public string GuardianArabicName { get; set; }
        public string GuardianEnglishName { get; set; }


    }
}
