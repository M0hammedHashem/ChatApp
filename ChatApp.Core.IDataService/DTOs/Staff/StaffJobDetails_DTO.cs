namespace ChatApp.Core.IDataService
{
    public class StaffJobDetails_DTO
    {
        public string StaffId { get; set; }
        public int? SchoolId { get; set; }
        public int? Department { get; set; }

        public Staff_DTO Staff { get; set; }
        public SchoolBranch_DTO School { get; set; }
    }
}
