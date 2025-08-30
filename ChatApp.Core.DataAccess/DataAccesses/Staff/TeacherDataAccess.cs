using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class TeacherDataAccess : BaseDataAccess<Teacher>, ITeacherDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public TeacherDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
