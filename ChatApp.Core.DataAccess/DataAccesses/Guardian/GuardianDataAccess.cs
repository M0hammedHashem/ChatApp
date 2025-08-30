using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class GuardianDataAccess : BaseDataAccess<Guardian>, IGuardianDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public GuardianDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}