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
        public IQueryable<Patient> GetAll();
        public IQueryable<Session> GetAllPatientSessions(int patientId);
        public Patient GetPatientById(int id);
        public void UpdatePatient(Patient patient);
        public void DeletePatient(int id);
        public void AddPatient(Patient patient);
        public Patient GetPatientBySN(string socialNumber);


    }
}
