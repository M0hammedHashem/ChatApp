using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class ChatRoomMembersDataAccess : BaseDataAccess<ChatRoomMembers>, IChatRoomMembersDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public ChatRoomMembersDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}