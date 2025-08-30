
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class SubjectChatRoomsDataService : BaseDataService<SubjectChatRooms, SubjectChatRooms_DTO>, ISubjectChatRoomsDataService
    {
        public SubjectChatRoomsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)

        {

        }
    }
}