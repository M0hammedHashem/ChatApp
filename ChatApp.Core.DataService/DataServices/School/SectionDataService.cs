using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class SectionDataService : BaseDataService<Section, Section_DTO>, ISectionDataService
    {
        public SectionDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}