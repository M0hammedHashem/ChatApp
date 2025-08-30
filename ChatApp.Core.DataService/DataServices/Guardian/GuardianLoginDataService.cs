using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    // In ChatApp.Core.DataService/StudentDataService.cs

    public class GuardianLoginDataService : BaseDataService<GuardianLogin, GuardianLogin_DTO>, IGuardianLoginDataService
    {
        public GuardianLoginDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            // ... any StudentDataService specific constructor logic
        }
    }
}