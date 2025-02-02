﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Data
{
    public class SMSystemsDBContext : DbContext
    {


        public SMSystemsDBContext(DbContextOptions<SMSystemsDBContext> options) : base(options)
        {
            var connectionString = this.Database.GetConnectionString();
            Console.WriteLine($"Current Connection String: {connectionString}");
        }


        public DbSet<Patient> Patients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<PatientReport> PatientReports { get; set; }

    
    }
}
