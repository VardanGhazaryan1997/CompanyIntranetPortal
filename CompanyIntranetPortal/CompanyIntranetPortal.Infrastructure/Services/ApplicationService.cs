using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public class ApplicationService : BaseService, IApplicationService
    {
        public ApplicationService(CompanyIntranetDBContext dbContext) : base(dbContext) { }

        public async Task Create(int userId, int applicationType, string description)
        {
            var application = new Application
            {
                Description = description,
                ApplicationType = await _dbContext.ApplicationTypes.FindAsync(applicationType),
                User = await _dbContext.Users.FindAsync(userId),
                ApplicationState = Core.Enums.ApplicationState.Pending,
            };

            _dbContext.Applications.Add(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Application application)
        {
            _dbContext.Applications.Remove(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteGetApplication(int id)
        {
            var application = await _dbContext.Applications.FindAsync(id);
            _dbContext.Remove(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Application> GetApplication(int id)
        {
            return await _dbContext.Applications
                .Include(a=>a.User)
                .Include(a => a.ApplicationType)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Application>> GetApplications()
        {
            return await _dbContext.Applications
                .Include(a => a.User)
                .Include(a => a.ApplicationType)
                .ToListAsync();
        }

        public async Task<List<ApplicationType>> GetApplicationTypes()
        {
            return await _dbContext.ApplicationTypes.ToListAsync();
        }

        public async Task Update(Application application)
        {
            _dbContext.Applications.Update(application);
            await _dbContext.SaveChangesAsync();
        }
    }
}
