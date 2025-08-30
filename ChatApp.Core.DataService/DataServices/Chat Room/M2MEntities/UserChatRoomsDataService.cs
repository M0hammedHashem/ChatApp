
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class UserChatRoomsDataService : BaseDataService<UserChatRooms, UserChatRooms_DTO>, IUserChatRoomsDataService
    {
        public UserChatRoomsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)

        {

        }
    }
}