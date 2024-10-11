using AutoMapper;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Services
{
    public class PatientReportService : IPatientReportService
    {
        private readonly IPatientReportRepository _patientReportRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;

        public PatientReportService(IPatientReportRepository patientReportService, IPatientRepository patientService, IMapper mapper)
        {
            _patientReportRepository = patientReportService;
            _patientRepository = patientService;
            _mapper = mapper;
        }
        public async Task AddPatientReport(PatientReport patientReport)
        {
            await _patientReportRepository.AddPatientReport(patientReport);
        }

        public async Task DeletePatientReport(PatientReport patientReport)
        {
            await _patientReportRepository.DeletePatientReport(patientReport);
        }

        public async Task<List<PatientReport>> GetAllPatientReports(int patientId)
        {
            return await _patientReportRepository.GetAllPatientReports(patientId);
        }

        public async Task<PatientReport> GetPatientReportById(int id)
        {
            return await _patientReportRepository.GetPatientReportById(id);
        }

        public async Task UpdatePatientReport(PatientReport patientReport)
        {
           await _patientReportRepository.UpdatePatientReport(patientReport);
        }
    }
}
