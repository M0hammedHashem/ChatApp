using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class StaffDataAccess : BaseDataAccess<Staff>, IStaffDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public StaffDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
