using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class CurriculumDataService : BaseDataService<Curriculum, Curriculum_DTO>, ICurriculumDataService
    {

        public CurriculumDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
