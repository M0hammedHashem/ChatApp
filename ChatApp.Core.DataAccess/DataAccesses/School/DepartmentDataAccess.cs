using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class DepartmentDataAccess : BaseDataAccess<Department>, IDepartmentDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public DepartmentDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
