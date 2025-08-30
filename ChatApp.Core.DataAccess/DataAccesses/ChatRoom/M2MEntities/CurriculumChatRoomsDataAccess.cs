using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class CurriculumChatRoomsDataAccess : BaseDataAccess<CurriculumChatRooms>, ICurriculumChatRoomsDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public CurriculumChatRoomsDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}