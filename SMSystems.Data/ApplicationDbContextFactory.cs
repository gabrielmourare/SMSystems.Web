using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<SMSystemsDBContext>
    {
        public SMSystemsDBContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
              .AddJsonFile($"appsettings.{environment}.json", optional: true)
             .Build();

            var connectionString = configuration.GetConnectionString("SMSystemsDev");

            var optionsBuilder = new DbContextOptionsBuilder<SMSystemsDBContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new SMSystemsDBContext(optionsBuilder.Options);
        }
    }
}
