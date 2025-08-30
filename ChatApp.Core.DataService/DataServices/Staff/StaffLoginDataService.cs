using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class StaffLoginDataService : BaseDataService<StaffLogin, StaffLogin_DTO>, IStaffLoginDataService
    {
        public StaffLoginDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
