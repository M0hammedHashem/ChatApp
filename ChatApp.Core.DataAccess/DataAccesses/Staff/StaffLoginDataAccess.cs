using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class StaffLoginDataAccess : BaseDataAccess<StaffLogin>, IStaffLoginDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public StaffLoginDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
