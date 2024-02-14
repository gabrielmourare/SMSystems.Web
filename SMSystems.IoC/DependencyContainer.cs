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
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<ISessionRepository, SessionRepository>();
           
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
