using CompanyIntranetPortal.Core.Entities;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public interface IApplicationService
    {
        public Task<List<Application>> GetApplications();
        public Task<Application> GetApplication(int id);
        public Task Update(Application application);
        public Task Delete(Application application);
        Task<List<ApplicationType>> GetApplicationTypes();
        Task Create(int userId, int applicationType, string description);
        Task DeleteGetApplication(int id);
    }
}
