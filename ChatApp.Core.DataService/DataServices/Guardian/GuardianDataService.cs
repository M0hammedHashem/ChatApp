using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    // In ChatApp.Core.DataService/StudentDataService.cs

    public class GuardianDataService : BaseDataService<Guardian, Guardian_DTO>, IGuardianDataService
    {
        public GuardianDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            // ... any StudentDataService specific constructor logic
        }
    }
}