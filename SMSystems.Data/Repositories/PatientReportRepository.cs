using Microsoft.EntityFrameworkCore;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Data.Repositories
{
    class PatientReportRepository : IPatientReportRepository, IDisposable
    {
        private readonly SMSystemsDBContext _context;
        private bool _disposed = false;

        public PatientReportRepository(SMSystemsDBContext context)
        {
            _context = context;
        }
        public async Task AddPatientReport(PatientReport patientReport)
        {
            await _context.AddAsync(patientReport);
            _context.SaveChanges();
        }

        public async Task DeletePatientReport(PatientReport patientReport)
        {
            if (patientReport != null)
            {
                _context.Remove(patientReport);
                await _context.SaveChangesAsync();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public async Task<List<PatientReport>> GetAllPatientReports(int patientId)
        {
            return await _context.PatientReports.Where(patientReport => patientReport.PatientId.Equals(patientId)).ToListAsync();

        }
        public async Task<PatientReport?> GetPatientReportById(int id)
        {
            return await _context.PatientReports.FindAsync(id);
        }

        public async Task UpdatePatientReport(PatientReport patientReport)
        {
            if (patientReport != null)
            {
                _context.Update(patientReport);
                await _context.SaveChangesAsync();
            }

        }
    }
}
