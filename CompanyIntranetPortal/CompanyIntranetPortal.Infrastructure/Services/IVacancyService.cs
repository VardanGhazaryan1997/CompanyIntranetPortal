using CompanyIntranetPortal.Core.Entities;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public interface IVacancyService
    {
        Task<List<Vacancy>> GetAll();
        Task CreateVacancy(Vacancy model);
        Task<Vacancy> GetVacancy(int id);
        Task UpdateVacancy(int id, Vacancy model);
        Task DeleteVacancy(int id);
    }
}
