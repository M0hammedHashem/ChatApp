using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class SchoolBranchDataService : BaseDataService<SchoolBranch, SchoolBranch_DTO>, ISchoolBranchDataService
    {

        public SchoolBranchDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
