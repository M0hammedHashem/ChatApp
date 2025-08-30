using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    // In ChatApp.Core.DataService/StudentDataService.cs

    public class StudentSchoolDetailsDataService : BaseDataService<StudentSchoolDetails, StudentSchoolDetails_DTO>, IStudentSchoolDetailsDataService
    {
        public StudentSchoolDetailsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            // ... any StudentDataService specific constructor logic
        }
    }
}