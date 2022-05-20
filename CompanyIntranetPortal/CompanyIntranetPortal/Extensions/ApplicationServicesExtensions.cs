using CompanyIntranet;
using CompanyIntranetPortal.Infrastructure;
using CompanyIntranetPortal.Infrastructure.Services;
using EnrollmentPortal.Infrastructure.Encryption;

namespace CompanyIntranetPortal.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IBooksService, BooksService>();
            services.AddScoped<IServiceDeskService, ServiceDeskService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IEFormsService, EFormsService>();
            services.AddScoped<IVacancyService, VacancyService>();

            services.AddScoped<IEmailService, EmailServcie>();
        }
    }
}
