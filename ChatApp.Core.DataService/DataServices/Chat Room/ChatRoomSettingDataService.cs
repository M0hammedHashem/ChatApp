
using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class ChatRoomSettingsDataService : BaseDataService<ChatRoomSettings, ChatRoomSettings_DTO>, IChatRoomSettingsDataService
    {
        public ChatRoomSettingsDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}