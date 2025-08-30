using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess.DataAccesses
{
    internal class SchoolBranchDataAccess : BaseDataAccess<SchoolBranch>, ISchoolBranchDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public SchoolBranchDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
