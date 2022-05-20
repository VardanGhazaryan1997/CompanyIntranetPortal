using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public class VacancyService : BaseService, IVacancyService
    {
        public VacancyService(CompanyIntranetDBContext dbContext) : base(dbContext)
        {
        }

        public async Task CreateVacancy(Vacancy model)
        {
            _dbContext.Vacancies.Add(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteVacancy(int id)
        {
            var vacancy = await GetVacancy(id);
            _dbContext.Remove(vacancy);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Vacancy>> GetAll()
        {
            return await _dbContext.Vacancies.ToListAsync();
        }

        public async Task<Vacancy> GetVacancy(int id)
        {
            return await _dbContext.Vacancies.FindAsync(id);
        }

        public async Task UpdateVacancy(int id, Vacancy model)
        {
            var vacancy =await GetVacancy(id);
            vacancy.DueDate = model.DueDate;
            vacancy.JobDescription = model.JobDescription;
            vacancy.PositionName = model.PositionName;
            vacancy.Updated = DateTime.Now;

            _dbContext.Update(vacancy);
            await _dbContext.SaveChangesAsync();
        }
    }
}
