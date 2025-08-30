namespace ChatApp.Core.IDataService
{
    public class StudentLogin_DTO
    {

        public int LoginId { get; set; }
        public string StudentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int SchoolId { get; set; }
        public Student_DTO Student { get; set; }
        public SchoolBranch_DTO School { get; set; }
    }
}
