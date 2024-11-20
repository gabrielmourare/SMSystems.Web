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

namespace SMSystems.UI.Pages.Billings
{
    public class CreateModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IContractService _contractService;
        private readonly IBillingService _billingService;

        public CreateModel(IPatientService patientService, IContractService contractService, IBillingService billingService)
        {
            _billingService = billingService;
            _contractService = contractService;
            _patientService = patientService;
        }

        [BindProperty]
        public List<DateTime> SessionDates { get; set; } = new List<DateTime>();

       

        public async Task<IActionResult> OnGet()
        {
            await PopulatePatientsDropdown();
            return Page();
        }

        [BindProperty]
        public Billing Billing { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

          
            return RedirectToPage("./Index");
        }
        private async Task PopulatePatientsDropdown()
        {
            var patients = await _patientService.GetAll();
            ViewData["PatientID"] = new SelectList(patients, "ID", "Name");
        }
    }
}
