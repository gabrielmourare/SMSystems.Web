using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;


namespace SMSystems.UI.Pages.PatientReports
{
    public class IndexModel : PageModel
    {
        private readonly IPatientReportService _patientReport;
        private readonly IPatientService _patientService;

        public IndexModel(IPatientReportService patientReport, IPatientService patientService)
        {
            _patientReport = patientReport;
            _patientService = patientService;
        }
        public Patient Patient { get; set; }
        public IList<PatientReport> PatientReport { get; set; } = default!;
        public string PatientName { get; set; }

        public async Task<IActionResult> OnGetAsync(int patientId)
        {
            if (patientId != 0)
            {
                if (patientId == null)
                {
                    return NotFound();
                }

                var patientreport = await _patientReport.GetAllPatientReports(patientId);

                if (patientreport == null)
                {
                    return NotFound();
                }
                else
                {
                    var patient = await _patientService.GetPatientById(patientId);
                    PatientReport = patientreport;
                    Patient = patient;
                    PatientName = patient.Name;
                }
                return Page();
            }
            else
            {
                PatientReport = new List<PatientReport>();
                Patient = new Patient();
                return Page();
            }
        }
    }
}
