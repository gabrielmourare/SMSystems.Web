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
            var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json")
             .Build();

            var connectionString = configuration.GetConnectionString("SMSystemsProd");

            var optionsBuilder = new DbContextOptionsBuilder<SMSystemsDBContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new SMSystemsDBContext(optionsBuilder.Options);
        }
    }
}
