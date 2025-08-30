
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class ChatRoomMessageDataService : BaseDataService<ChatRoomMessage, ChatRoomMessage_DTO>, IChatRoomMessageDataService
    {
        public ChatRoomMessageDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}