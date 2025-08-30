
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class ChatRoomUserDataService : BaseDataService<ChatRoomUser, ChatRoomUser_DTO>, IChatRoomUserDataService
    {
        public ChatRoomUserDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}