using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Core.DbContextManager
{
    public class Curriculum
    {
        [Column("CurriculumID")]
        public int CurriculumId { get; set; }
        public string? CurriculumArabicName { get; set; }
        public string? CurriculumEnglishName { get; set; }

        [Column("SchoolID")]
        public int? SchoolId { get; set; }
        public ICollection<CurriculumChatRooms> ChatRooms { get; set; } = new List<CurriculumChatRooms>();
        public ICollection<CurriculumDepartment> Departments { get; set; } = new List<CurriculumDepartment>();

    }

}
