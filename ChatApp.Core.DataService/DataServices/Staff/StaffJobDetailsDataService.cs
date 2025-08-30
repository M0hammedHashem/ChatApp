using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class StaffJobDetailsDataService : BaseDataService<StaffJobDetails, StaffJobDetails_DTO>, IStaffJobDetailsDataService
    {
        public StaffJobDetailsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
