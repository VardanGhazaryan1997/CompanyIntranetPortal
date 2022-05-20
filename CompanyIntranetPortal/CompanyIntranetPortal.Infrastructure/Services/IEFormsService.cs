using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Core.Enums;
using CompanyIntranetPortal.Infrastructure.Models;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public interface IEFormsService
    {
        Task BankAccountChange(int userId, string bankName, string newAccountNumber, string reason);
        Task CreateVacationApplication(int userId, DateTime startDate, int daysCount, int substitute);
        Task CreateDayOffApplication(int userId, DateTime date, int substitute);
        Task CreateJobLeftApplication(int userId, DateTime date, string reason);
        Task<List<ApplicationCommonModel>> GetAllApplications(int userId);
        Task<List<VacationApplication>> GetVacationRequests();
        Task<List<DayOffApplication>> GetDayoffRequests();
        Task<List<JobLeftApplication>> GetJobLeftRequests();
        Task<List<BankAccountChangeApplication>> GetBankAccountChangeRequests();
        Task<VacationApplication> GetVacationRequest(int id);
        Task VocationRequestDelete(int id);
        Task<DayOffApplication> GetDayoffRequest(int id);
        Task DayoffRequestDelete(int id);
        Task<JobLeftApplication> GetJobLeftRequest(int id);
        Task JobLeftRequestDelete(int id);
        Task<BankAccountChangeApplication> GetBankAccountChangeRequest(int id);
        Task BankAccountChangeRequestDelete(int id);
        Task VocationUpdate(int id, ApplicationState state);
        Task DayoffUpdate(int id, ApplicationState state);
        Task JobLeftUpdate(int id, ApplicationState state);
        Task BankAccountUpdate(int id, ApplicationState state);
        Task ApplicationUpdate(int id, ApplicationState state);
        Task TicketUpdate(int id, TicketStates state);
    }
}
