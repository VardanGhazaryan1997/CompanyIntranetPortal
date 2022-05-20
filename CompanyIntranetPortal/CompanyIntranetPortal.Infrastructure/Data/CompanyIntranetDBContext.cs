using CompanyIntranetPortal.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyIntranetPortal.Infrastructure.Data
{
    public class CompanyIntranetDBContext : DbContext
    {
        public CompanyIntranetDBContext(DbContextOptions<CompanyIntranetDBContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> PostLikes { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<TicketIssue> TicketIssues { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<BankAccountChangeApplication> BankAccountChangeApplications { get; set; }
        public DbSet<VacationApplication> VacationApplications { get; set; }
        public DbSet<DayOffApplication> DayOffApplications { get; set; }
        public DbSet<JobLeftApplication> JobLeftApplications { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }

    }
}
