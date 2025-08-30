using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class CurriculumDepartmentDataService : BaseDataService<CurriculumDepartment, CurriculumDepartment_DTO>, ICurriculumDepartmentDataService
    {

        public CurriculumDepartmentDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
