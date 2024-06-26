using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
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
            var optionsBuilder = new DbContextOptionsBuilder<SMSystemsDBContext>();
            optionsBuilder.UseSqlServer("Server=G3B4RB4\\SQLEXPRESS;Database=SMSystemsDev;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;");

            return new SMSystemsDBContext(optionsBuilder.Options);
        }
    }
}
