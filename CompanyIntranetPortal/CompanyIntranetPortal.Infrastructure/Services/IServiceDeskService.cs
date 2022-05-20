using CompanyIntranetPortal.Core.Entities;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public interface IServiceDeskService
    {
        Task<List<Ticket>> GetUserServiceTickets(int userId);
        Task<List<TicketType>> GetTicketTypes();
        Task<List<TicketIssue>> GetIssues(int typeid);
        Task CreateTicket(string contactPhone, string description, int issueId, int typeid, int userId);
        Task<Ticket?> GetTicket(int id);
        Task<List<Ticket>> GetAllTickets();
        Task DeleteTicket(int id);
    }
}
