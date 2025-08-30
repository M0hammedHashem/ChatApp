
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;


namespace ChatApp.Core.DataAccess
{
    public class ClassChatRoomsDataAccess : BaseDataAccess<ClassChatRooms>, IClassChatRoomsDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public ClassChatRoomsDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}