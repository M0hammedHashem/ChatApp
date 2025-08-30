using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    // In ChatApp.Core.DataService/StudentDataService.cs

    public class StudentLoginDataService : BaseDataService<StudentLogin, StudentLogin_DTO>, IStudentLoginDataService
    {
        public StudentLoginDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            // ... any StudentDataService specific constructor logic
        }
    }
}