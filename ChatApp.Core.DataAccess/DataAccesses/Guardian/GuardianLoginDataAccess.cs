using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class GuardianLoginDataAccess : BaseDataAccess<GuardianLogin>, IGuardianLoginDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public GuardianLoginDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
