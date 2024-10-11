using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.PatientReports
{
    public class DeleteModel : PageModel
    {
        private readonly IPatientReportService _patientReportService;

        public DeleteModel(IPatientReportService patientReportService)
        {
            _patientReportService = patientReportService;
        }

        [BindProperty]
        public PatientReport PatientReport { get; set; } = default!;
        [BindProperty]
        public int PatientIdSelected { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientreport = await _patientReportService.GetPatientReportById(id);

            if (patientreport == null)
            {
                return NotFound();
            }
            else
            {
                PatientReport = patientreport;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientreport = await _patientReportService.GetPatientReportById(id);
            if (patientreport != null)
            {
                PatientReport = patientreport;
                PatientIdSelected = PatientReport.PatientId;
                await _patientReportService.DeletePatientReport(PatientReport);
            }

            return RedirectToPage("./Index", new { patientId = PatientIdSelected });
        }
    }
}
