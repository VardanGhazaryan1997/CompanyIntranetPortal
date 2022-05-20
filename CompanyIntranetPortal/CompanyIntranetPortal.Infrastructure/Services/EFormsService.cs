using CompanyIntranetPortal.Core.Entities;
using CompanyIntranetPortal.Core.Enums;
using CompanyIntranetPortal.Infrastructure.Data;
using CompanyIntranetPortal.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Services
{
    public class EFormsService : BaseService, IEFormsService
    {
        public EFormsService(CompanyIntranetDBContext dbContext) : base(dbContext) { }

        public async Task BankAccountChange(int userId, string bankName, string newAccountNumber, string reason)
        {
            var application = new BankAccountChangeApplication
            {
                ApplicationState = Core.Enums.ApplicationState.Pending,
                BankName = bankName,
                NewAccountNumber = newAccountNumber,
                Reason = reason,
                UserId = userId,
                User = await _dbContext.Users.FindAsync(userId),
            };

            _dbContext.BankAccountChangeApplications.AddAsync(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateDayOffApplication(int userId, DateTime date, int substitute)
        {
            var application = new DayOffApplication
            {
                ApplicationState = Core.Enums.ApplicationState.Pending,
                Substitute = substitute,
                Date = date,
                UserId = userId,
                User = await _dbContext.Users.FindAsync(userId),
            };

            _dbContext.DayOffApplications.AddAsync(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateJobLeftApplication(int userId, DateTime date, string reason)
        {
            var application = new JobLeftApplication
            {
                ApplicationState = Core.Enums.ApplicationState.Pending,
                Date = date,
                Reason = reason,
                UserId = userId,
                User = await _dbContext.Users.FindAsync(userId),
            };

            _dbContext.JobLeftApplications.AddAsync(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateVacationApplication(int userId, DateTime startDate, int daysCount, int substitute)
        {
            var application = new VacationApplication
            {
                ApplicationState = Core.Enums.ApplicationState.Pending,
                DaysCount = daysCount,
                Substitute = substitute,
                StartDate = startDate,
                UserId = userId,
                User = await _dbContext.Users.FindAsync(userId),
            };

            _dbContext.VacationApplications.AddAsync(application);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationCommonModel>> GetAllApplications(int userId)
        {
            List<ApplicationCommonModel> result = new List<ApplicationCommonModel>();
            var bankAccountapp = await _dbContext.BankAccountChangeApplications.Where(a => a.UserId == userId).ToListAsync();
            var dayOffapp = await _dbContext.DayOffApplications.Where(a => a.UserId == userId).ToListAsync();
            var jobLeftAapp = await _dbContext.JobLeftApplications.Where(a => a.UserId == userId).ToListAsync();
            var vacationapp = await _dbContext.VacationApplications.Where(a => a.UserId == userId).ToListAsync();

            result.AddRange(bankAccountapp.Select(a => new ApplicationCommonModel { Created = a.Created, ApplicationState = a.ApplicationState, Type = "Change Bank Account" }));
            result.AddRange(dayOffapp.Select(a => new ApplicationCommonModel { Created = a.Created, ApplicationState = a.ApplicationState, Type = "Dayoff" }));
            result.AddRange(jobLeftAapp.Select(a => new ApplicationCommonModel { Created = a.Created, ApplicationState = a.ApplicationState, Type = "Job Left" }));
            result.AddRange(vacationapp.Select(a => new ApplicationCommonModel { Created = a.Created, ApplicationState = a.ApplicationState, Type = "Vacation" }));

            result.OrderBy(a => a.Created);

            return result;
        }

        public async Task<List<BankAccountChangeApplication>> GetBankAccountChangeRequests()
        {
            return await _dbContext.BankAccountChangeApplications.Include(a => a.User).ToListAsync();
        }

        public async Task<List<DayOffApplication>> GetDayoffRequests()
        {
            return await _dbContext.DayOffApplications.Include(a => a.User).ToListAsync();
        }

        public async Task<List<JobLeftApplication>> GetJobLeftRequests()
        {
            return await _dbContext.JobLeftApplications.Include(a => a.User).ToListAsync();
        }

        public async Task<VacationApplication> GetVacationRequest(int id)
        {
            return await _dbContext.VacationApplications
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<VacationApplication>> GetVacationRequests()
        {
            return await _dbContext.VacationApplications.Include(a => a.User).ToListAsync();
        }

        public async Task VocationRequestDelete(int id)
        {
            var request = await _dbContext.VacationApplications.FindAsync(id);
            _dbContext.Remove(request);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<DayOffApplication> GetDayoffRequest(int id)
        {
            return await _dbContext.DayOffApplications
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task DayoffRequestDelete(int id)
        {
            var request = await _dbContext.DayOffApplications.FindAsync(id);
            _dbContext.Remove(request);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<JobLeftApplication> GetJobLeftRequest(int id)
        {
            return await _dbContext.JobLeftApplications
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task JobLeftRequestDelete(int id)
        {
            var request = await _dbContext.JobLeftApplications.FindAsync(id);
            _dbContext.Remove(request);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<BankAccountChangeApplication> GetBankAccountChangeRequest(int id)
        {
            return await _dbContext.BankAccountChangeApplications
               .Include(r => r.User)
               .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task BankAccountChangeRequestDelete(int id)
        {
            var request = await _dbContext.BankAccountChangeApplications.FindAsync(id);
            _dbContext.Remove(request);

            await _dbContext.SaveChangesAsync();
        }

        public async Task VocationUpdate(int id, ApplicationState state)
        {
            var request = await _dbContext.VacationApplications.FindAsync(id);
            request.ApplicationState = state;
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DayoffUpdate(int id, ApplicationState state)
        {
            var request = await _dbContext.DayOffApplications.FindAsync(id);
            request.ApplicationState = state;
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task JobLeftUpdate(int id, ApplicationState state)
        {
            var request = await _dbContext.JobLeftApplications.FindAsync(id);
            request.ApplicationState = state;
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task BankAccountUpdate(int id, ApplicationState state)
        {
            var request = await _dbContext.BankAccountChangeApplications.FindAsync(id);
            request.ApplicationState = state;
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ApplicationUpdate(int id, ApplicationState state)
        {
            var request = await _dbContext.Applications.FindAsync(id);
            request.ApplicationState = state;
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
        }

        public async Task TicketUpdate(int id, TicketStates state)
        {
            var request = await _dbContext.Tickets.FindAsync(id);
            request.TicketState = state;
            _dbContext.Update(request);
            await _dbContext.SaveChangesAsync();
        }
    }
}
