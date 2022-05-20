using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Core.Enums;
using CompanyIntranetPortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public class ServiceDeskService : BaseService, IServiceDeskService
    {
        public ServiceDeskService(CompanyIntranetDBContext dbContext) : base(dbContext) { }

        public async Task CreateTicket(string contactPhone, string description, int issueId, int typeid, int userId)
        {
            var ticket = new Ticket();
            ticket.TicketIssue = await _dbContext.TicketIssues.FindAsync(issueId);
            ticket.TicketType = await _dbContext.TicketTypes.FindAsync(typeid);
            ticket.Description = description;
            ticket.ContactPhone = contactPhone;
            ticket.TicketState = TicketStates.Pending;
            ticket.User = await _dbContext.Users.FindAsync(userId);

            _dbContext.Tickets.Add(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTicket(int id)
        {
            var ticket = await _dbContext.Tickets.FindAsync(id);
            _dbContext.Tickets.Remove(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _dbContext
                .Tickets
                .Include(t => t.TicketIssue)
                .Include(t => t.TicketType)
                .Include(t => t.User)
                .ToListAsync();
        }

        public async Task<List<TicketIssue>> GetIssues(int typeid)
        {
            return await _dbContext.TicketIssues
                .Include(i => i.TicketType)
                .Where(i => i.TicketType.Id == typeid)
                .ToListAsync();
        }

        public async Task<Ticket?> GetTicket(int id)
        {
            return await _dbContext
                .Tickets
                .Include(t => t.TicketIssue)
                .Include(t => t.TicketType)
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TicketType>> GetTicketTypes()
        {
            return await _dbContext
                .TicketTypes
                .ToListAsync();
        }

        public async Task<List<Ticket>> GetUserServiceTickets(int userId)
        {
            return await _dbContext
                    .Tickets
                    .Include(t => t.TicketIssue)
                    .Include(t => t.TicketType)
                    .Include(t => t.User)
                    .Where(t => t.User.Id == userId)
                    .ToListAsync();
        }
    }
}
