namespace ChatApp.Core.IDataService
{
    public class Curriculum_DTO
    {
        public int CurriculumId { get; set; }
        public string? CurriculumArabicName { get; set; }
        public string? CurriculumEnglishName { get; set; }
        public int? SchoolId { get; set; }
        public SchoolBranch_DTO? School { get; set; }
        public ICollection<CurriculumChatRooms_DTO> ChatRooms { get; set; } = new List<CurriculumChatRooms_DTO>();
        public ICollection<CurriculumDepartment_DTO> Departments { get; set; } = new List<CurriculumDepartment_DTO>();

    }
}
