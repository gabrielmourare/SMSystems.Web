using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Interfaces
{
    public interface IPatientService
    {
        IQueryable<Invoice> ObtainAllInvoices(int patientId);
        Task<List<Session>> GetAllPatientSessions(int patientId);
        Task<Patient?> GetPatientData(int patientId);
        Task<List<Patient>> GetAll();
        Task<Patient?> GetPatientById(int id);
        Task UpdatePatient(Patient patient);
        Task DeletePatient(Patient patient);
        Task AddPatient(Patient patientViewModel);
        Task<Patient?> GetPatientBySN(string socialNumber);
        Task<bool> PatientExistsAsync(int id);
        Task<Contract> GetPatientContract(int contractId);
    }

}
