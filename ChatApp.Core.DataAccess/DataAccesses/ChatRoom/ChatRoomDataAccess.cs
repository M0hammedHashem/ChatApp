using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class ChatRoomDataAccess : BaseDataAccess<ChatRoom>, IChatRoomDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public ChatRoomDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}