using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{

    internal class ChatRoomSettingDataAccess : BaseDataAccess<ChatRoomSettings>, IChatRoomSettingDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public ChatRoomSettingDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}