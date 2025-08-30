using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class SubjectDataAccess : BaseDataAccess<Subject>, ISubjectDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public SubjectDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
