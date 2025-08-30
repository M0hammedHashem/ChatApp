using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class UserChatRoomsDataAccess : BaseDataAccess<UserChatRooms>, IUserChatRoomsDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public UserChatRoomsDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}