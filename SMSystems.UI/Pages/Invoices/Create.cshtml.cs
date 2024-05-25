using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPatientService _patientService;

        public CreateModel(IInvoiceService invoiceService, IPatientService patientService)
        {
            _invoiceService = invoiceService;
            _patientService = patientService;
        }

        public IActionResult OnGet()
        {
            PopulatePatientsDropdown();
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        [BindProperty]
        public List<DateTime> SessionDates { get; set; } = new List<DateTime>();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Invoice.Sessions = new List<Session>();            

            // Add the sessions to the invoice
            foreach (var date in SessionDates)
            {
                Invoice.Sessions.Add(new Session { Date = date, PatientID = Invoice.PatientID, Value = Invoice.SessionValue });
                Invoice.TotalValue += Invoice.SessionValue;
            }

            Invoice.EmissionDate = DateTime.Now.Date;
         

            await _invoiceService.AddInvoiceAsync(Invoice);


            return RedirectToPage("./Index");
        }

        private void PopulatePatientsDropdown()
        {
            var patients = _patientService.GetAll();
            ViewData["PatientID"] = new SelectList(patients, "ID", "Name");
        }
    }
}
