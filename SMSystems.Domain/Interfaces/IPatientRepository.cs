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
        IQueryable<Patient> GetAllPatients();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task SavePatientAsync(Patient patient);
        Task DeletePatientAsync(int patientId);
        Task UpdatePatientAsync(Patient updatedPatient);
        Task<Patient?> GetPatientBySocialNumberAsync(string socialNumber);
        Task<bool> PatientExistsAsync(int id);

    }
}
