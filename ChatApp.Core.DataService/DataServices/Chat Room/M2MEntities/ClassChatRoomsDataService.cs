
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class ClassChatRoomsDataService : BaseDataService<ClassChatRooms, ClassChatRooms_DTO>, IClassChatRoomsDataService
    {
        public ClassChatRoomsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)

        {

        }
    }
}