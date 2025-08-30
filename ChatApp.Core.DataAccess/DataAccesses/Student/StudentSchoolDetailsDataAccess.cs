using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class StudentSchoolDetailsDataAccess : BaseDataAccess<StudentSchoolDetails>, IStudentSchoolDetailsDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public StudentSchoolDetailsDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}