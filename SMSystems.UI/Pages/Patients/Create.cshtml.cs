using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly IPatientService _patientService;

        public CreateModel(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await _patientService.AddPatient(Patient);
            }
            catch (Exception ex)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
