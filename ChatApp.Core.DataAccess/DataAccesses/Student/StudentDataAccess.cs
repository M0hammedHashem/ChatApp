using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class StudentDataAccess : BaseDataAccess<Student>, IStudentDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public StudentDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}