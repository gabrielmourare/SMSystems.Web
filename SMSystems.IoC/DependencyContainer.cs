using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using SMSystems.Application.Services;
using SMSystems.Domain.Interfaces;
using SMSystems.Data.Repositories;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace SMSystems.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ISessionRepository, SessionRepository>();
           
            return services;
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SMSystemsDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SMSystemsDevelopment"));


            });
           
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

    }

   
}
