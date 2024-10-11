using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.PatientReports
{
    public class CreateModel : PageModel
    {
        private readonly IPatientReportService _patientReportService;
        private readonly IPatientService _patientService;

        public CreateModel(IPatientReportService patientReportService, IPatientService patientService)
        {
            _patientReportService = patientReportService;
            _patientService = patientService;
        }

        [BindProperty]
        public int PatientIdSelected { get; set; }

        public string PatientName;

        [BindProperty]
        public DateTime ReportDate { get; set; }

        [BindProperty]
        public PatientReport PatientReport { get; set; } = default!;

        public bool PatientExists;





        public async Task<IActionResult> OnGetAsync(int patientId)
        {
            if (patientId != 0)
            {
                PatientExists = true;
                // Armazena o patientId para uso na criação
                PatientIdSelected = patientId;
                ReportDate = DateTime.Now;

                var patient = await _patientService.GetPatientById(patientId);

                if (patient != null)
                {
                    PatientName = patient.Name;
                }


                return Page();
            }
            else
            {

                await PopulatePatientsDropdown();
                PatientExists = false;
                ReportDate = DateTime.Now;
                return Page();
            }
        }



        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }
            // Verifica se o PatientId foi selecionado no dropdown
            if (PatientIdSelected == 0)
            {
                ModelState.AddModelError("PatientId", "Por favor, selecione um paciente.");
                await PopulatePatientsDropdown(); // Recarrega a lista de pacientes
                return Page();
            }

            // Preenche os dados do relatório do paciente
            PatientReport.ReportDate = ReportDate;
            PatientReport.PatientId = PatientIdSelected;          

            // Obtém o conteúdo do editor no formato Delta
           
            await _patientReportService.AddPatientReport(PatientReport);

            return RedirectToPage("./Index", new { patientId = PatientIdSelected });
        }

        private async Task PopulatePatientsDropdown()
        {
            var patients = await _patientService.GetAll();
            ViewData["PatientIDSelected"] = new SelectList(patients, "ID", "Name");
        }
    }
}
