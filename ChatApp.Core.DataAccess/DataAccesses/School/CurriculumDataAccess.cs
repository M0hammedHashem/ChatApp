using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class CurriculumDataAccess : BaseDataAccess<Curriculum>, ICurriculumDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public CurriculumDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
