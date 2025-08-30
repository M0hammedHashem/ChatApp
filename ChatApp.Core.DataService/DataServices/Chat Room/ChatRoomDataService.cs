
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class ChatRoomDataService : BaseDataService<ChatRoom, ChatRoom_DTO>, IChatRoomDataService
    {
        public ChatRoomDataService(IUnitOfWork unitOfWork) : base(unitOfWork)

        {

        }
    }
}