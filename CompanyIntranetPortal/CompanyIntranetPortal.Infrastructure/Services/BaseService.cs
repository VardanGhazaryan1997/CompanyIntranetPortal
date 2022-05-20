using CompanyIntranetPortal.Infrastructure.Data;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public abstract class BaseService
    {
        protected CompanyIntranetDBContext _dbContext;
        public BaseService(CompanyIntranetDBContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
