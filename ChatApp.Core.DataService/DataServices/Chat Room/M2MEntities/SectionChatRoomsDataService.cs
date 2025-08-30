
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class SectionChatRoomsDataService : BaseDataService<SectionChatRooms, SectionChatRooms_DTO>, ISectionChatRoomsDataService
    {
        public SectionChatRoomsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)

        {

        }
    }
}