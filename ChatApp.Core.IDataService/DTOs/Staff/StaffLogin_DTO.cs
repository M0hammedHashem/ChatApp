using ChatApp.Core.DbContextManager;

namespace ChatApp.Core.IDataService
{
    public class StaffLogin_DTO
    {


        public int UserId { get; set; }

        public string StaffId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public int SchoolId { get; set; }

        public StaffType StaffType { get; set; }
        public SchoolBranch_DTO School { get; set; }
        public Staff_DTO Staff { get; set; }

    }
}
