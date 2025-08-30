using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class SectionChatRoomsDataAccess : BaseDataAccess<SectionChatRooms>, ISectionChatRoomsDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public SectionChatRoomsDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}