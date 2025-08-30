using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class CurriculumDepartmentDataAccess : BaseDataAccess<CurriculumDepartment>, ICurriculumDepartmentDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public CurriculumDepartmentDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
