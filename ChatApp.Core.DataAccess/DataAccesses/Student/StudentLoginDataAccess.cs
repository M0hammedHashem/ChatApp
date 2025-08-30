using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class StudentLoginDataAccess : BaseDataAccess<StudentLogin>, IStudentLoginDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public StudentLoginDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}