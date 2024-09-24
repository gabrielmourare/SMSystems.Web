using AutoMapper;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Services
{
    public class PatientReportService : IPatientReportService
    {
        private readonly IPatientReportService _patientReportService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;

        public PatientReportService(IPatientReportService patientReportService, IPatientService patientService, IMapper mapper)
        {
            _patientReportService = patientReportService;
            _patientService = patientService;
            _mapper = mapper;
        }
        public async Task AddPatientReport(PatientReport patientReport)
        {
            await _patientReportService.AddPatientReport(patientReport);
        }

        public async Task DeletePatientReport(PatientReport patientReport)
        {
            await _patientReportService.DeletePatientReport(patientReport);
        }

        public async Task<List<PatientReport>> GetAllPatientReports(int patientId)
        {
            return await _patientReportService.GetAllPatientReports(patientId);
        }

        public async Task<PatientReport> GetPatientReportById(int id)
        {
            return await _patientReportService.GetPatientReportById(id);
        }

        public async Task UpdatePatientReport(PatientReport patientReport)
        {
           await _patientReportService.UpdatePatientReport(patientReport);
        }
    }
}
