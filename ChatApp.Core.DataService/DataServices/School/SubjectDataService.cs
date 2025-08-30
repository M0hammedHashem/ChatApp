using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class SubjectDataService : BaseDataService<Subject, Subject_DTO>, ISubjectDataService
    {
        public SubjectDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
