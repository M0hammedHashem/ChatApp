using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    // In ChatApp.Core.DataService/StudentDataService.cs

    public class TeacherDataService : BaseDataService<Teacher, Teacher_DTO>, ITeacherDataService
    {
        public TeacherDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            // ... any StudentDataService specific constructor logic
        }
    }
}