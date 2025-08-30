
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class ChatRoomMembersDataService : BaseDataService<ChatRoomMembers, ChatRoomMembers_DTO>, IChatRoomMembersDataService
    {
        public ChatRoomMembersDataService(IUnitOfWork unitOfWork) : base(unitOfWork)

        {

        }
    }
}