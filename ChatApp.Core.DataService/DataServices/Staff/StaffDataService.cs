using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class StaffDataService : BaseDataService<Staff, Staff_DTO>, IStaffDataService
    {
        public StaffDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
