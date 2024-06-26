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
    public class PatientRepository : IPatientRepository, IDisposable
    {
        private readonly SMSystemsDBContext _context;
        private bool _disposed = false;

        public PatientRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public async Task DeletePatientAsync(Patient patient)
        {           
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await SaveAsync();
            }
        }

        public IQueryable<Patient> GetAllPatients()
        {
            return _context.Patients;
        }

        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task SavePatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await SaveAsync();
        }

        public async Task UpdatePatientAsync(Patient updatedPatient)
        {
            _context.Patients.Update(updatedPatient);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Patient?> GetPatientBySocialNumberAsync(string socialNumber)
        {
            return await _context.Patients.FirstOrDefaultAsync(patient => patient.SocialNumber == socialNumber);
        }

        public async Task<bool> PatientExistsAsync(int id)
        {
            return await _context.Patients.AnyAsync(e => e.ID == id);
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
    }
}
