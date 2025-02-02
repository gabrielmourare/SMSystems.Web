﻿using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task SavePatientAsync(Patient patient);
        Task DeletePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient updatedPatient);
        Task<Patient?> GetPatientBySocialNumberAsync(string socialNumber);
        Task<bool> PatientExistsAsync(int id);

        Task<Contract> GetContract(int contractId);
    }
}
