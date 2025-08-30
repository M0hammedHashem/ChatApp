using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{

    internal class ChatRoomUserDataAccess : BaseDataAccess<ChatRoomUser>, IChatRoomUserDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public ChatRoomUserDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}