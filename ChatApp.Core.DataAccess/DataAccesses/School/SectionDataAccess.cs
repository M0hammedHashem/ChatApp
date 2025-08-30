using ChatApp.Core.DbContextManager;
using ChatApp.Core.IDataAccess;

namespace ChatApp.Core.DataAccess
{
    internal class SectionDataAccess : BaseDataAccess<Section>, ISectionDataAccess
    {
        private readonly IChatApp_DbContext _dbContext;

        public SectionDataAccess(IChatApp_DbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}