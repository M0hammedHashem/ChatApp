using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;
using ChatApp.Core.IDataService;

namespace ChatApp.Core.DataService
{
    public class DepartmentDataService : BaseDataService<Department, Department_DTO>, IDepartmentDataService
    {

        public DepartmentDataService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
