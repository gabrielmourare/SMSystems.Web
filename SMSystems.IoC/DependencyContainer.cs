using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SMSystems.Application.Services;
using SMSystems.Domain.Interfaces;
using SMSystems.Data.Repositories;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using Microsoft.EntityFrameworkCore;

namespace SMSystems.IoC
{
    public class DependencyContainer
    {
        public static IConfiguration Configuration { get; set; }
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddDbContext<SMSystemsDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(""));
            });
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceRepository, IInvoiceRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionRepository, SessionRepository>();

        }
    }
}
