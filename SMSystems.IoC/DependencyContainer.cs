using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SMSystems.Application.Services;
using SMSystems.Domain.Interfaces;
using SMSystems.Data.Repositories;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using AutoMapper;
using System.Globalization;
using SMSystems.Application.Printer.Interfaces;
using SMSystems.Printer.Services;

namespace SMSystems.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IPrinterService, Application.Printer.Services.PrinterService>();
            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IPatientReportService, PatientReportService>();
            services.AddScoped<IPatientReportRepository, PatientReportRepository>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IBillingRepository, BillingRepository>();


            return services;
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SMSystemsDBContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("SMSystems"));
                
            }, ServiceLifetime.Scoped);         
       

        }

    }


}
