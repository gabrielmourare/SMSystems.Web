using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface IPatientReportRepository
    {
        Task<List<PatientReport>> GetAllPatientReports(int patientId);
        Task<PatientReport> GetPatientReportById(int id);
        Task AddPatientReport(PatientReport patientReport);
        Task DeletePatientReport(PatientReport patientReport);
        Task UpdatePatientReport(PatientReport patientReport);

    }
}
