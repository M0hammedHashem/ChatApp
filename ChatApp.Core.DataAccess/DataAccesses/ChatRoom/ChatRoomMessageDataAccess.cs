using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    public interface I : IBaseDataAccess<Student>
    {

    }
    internal class ChatRoomMessageDataAccess : BaseDataAccess<ChatRoomMessage>, IChatRoomMessageDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public ChatRoomMessageDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}