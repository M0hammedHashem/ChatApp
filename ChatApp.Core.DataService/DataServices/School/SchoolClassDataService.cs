using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class SchoolClassDataService : BaseDataService<SchoolClass, SchoolClass_DTO>, ISchoolClassDataService
    {
        public SchoolClassDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}