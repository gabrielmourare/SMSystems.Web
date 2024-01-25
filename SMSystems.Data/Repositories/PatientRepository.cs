using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        public SMSystemsDBContext _context;

        public PatientRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public void DeletePatient(int id)
        {
            _context.Patients.Remove(GetPatientById(id));
        }

        public IQueryable<Patient> GetAllPatients()
        {
            return _context.Patients;
        }

        public Patient? GetPatientById(int id)
        {
            return _context.Patients.Find(id);
        }

        public void SavePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();

        }

        public void UpdatePatient(int patientId)
        {
            _context.Patients.Update(GetPatientById(patientId));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public Patient? GetPatientBySocialNumber(string socialNumber)
        {
            return _context.Patients.Where(patient => patient.SocialNumber == socialNumber).FirstOrDefault();
        }
    }
}
