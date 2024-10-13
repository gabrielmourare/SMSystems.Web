using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Data;
using SMSystems.Domain.Entities;


namespace SMSystems.UI.Pages.PatientReports
{
    public class EditModel : PageModel
    {
        private readonly IPatientReportService _patientReportService;
        private readonly IPatientService _patientService;

        public EditModel(IPatientReportService patientReportService, IPatientService patientService)
        {
            _patientReportService = patientReportService;
            _patientService = patientService;
        }

        [BindProperty]
        public PatientReport PatientReport { get; set; } = default!;


        [BindProperty]
        public int PatientIdSelected { get; set; }
        public string PatientName { get; set; }


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

            if (patientreport.Content != null)
            {
                // Você deve criar um script para definir o conteúdo do Quill aqui
                ViewData["ContentJson"] = patientreport.Content; // Supondo que seja uma string JSON válida
            }

            var patient = await _patientService.GetPatientById(patientreport.PatientId);

            if (patient != null)
            {
                PatientName = patient.Name;
            }

            PatientIdSelected = patientreport.PatientId;
            PatientReport = patientreport;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            // Captura o conteúdo do Quill em formato Delta
            var deltaJson = Request.Form["contentJson"]; // Certifique-se de que o nome do campo oculto é 'contentJson'
            if (!string.IsNullOrEmpty(deltaJson))
            {
                // Deserializa o conteúdo Delta e atribui ao PatientReport.Content
                dynamic delta = JsonConvert.DeserializeObject(deltaJson);
                PatientReport.Content = JsonConvert.SerializeObject(delta);
            }

            PatientIdSelected = PatientReport.PatientId;

            await _patientReportService.UpdatePatientReport(PatientReport);

            return RedirectToPage("./Index", new { patientId = PatientIdSelected });
        }


    }
}
