
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class CurriculumChatRoomsDataService : BaseDataService<CurriculumChatRooms, CurriculumChatRooms_DTO>, ICurriculumChatRoomsDataService
    {
        public CurriculumChatRoomsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)

        {

        }
    }
}