using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class SchoolClassDataAccess : BaseDataAccess<SchoolClass>, ISchoolClassDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public SchoolClassDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
