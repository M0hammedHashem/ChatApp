namespace ChatApp.Core.IDataService
{
    public class GuardianLogin_DTO
    {

        public int WebUserId { get; set; }
        public string? GuardianId { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; }
        public int SchoolId { get; set; }
        public SchoolBranch_DTO School { get; set; }

    }
}