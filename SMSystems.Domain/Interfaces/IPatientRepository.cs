using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface IPatientRepository
    {
        public IQueryable<Patient> GetAllPatients();
        public Patient? GetPatientById(int id);
        public void SavePatient(Patient patient);
        public void DeletePatient(int patientId);
        public void UpdatePatient(int patientId);
        public Patient? GetPatientBySocialNumber(string socialNumber);

    }
}
