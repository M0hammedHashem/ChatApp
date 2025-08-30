using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class StaffJobDetailsDataAccess : BaseDataAccess<StaffJobDetails>, IStaffJobDetailsDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public StaffJobDetailsDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
