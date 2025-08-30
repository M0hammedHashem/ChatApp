using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class SubjectChatRoomsDataAccess : BaseDataAccess<SubjectChatRooms>, ISubjectChatRoomsDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public SubjectChatRoomsDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}