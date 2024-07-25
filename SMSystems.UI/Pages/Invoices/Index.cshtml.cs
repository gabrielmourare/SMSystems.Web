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
using SMSystems.UI.Pages.Patients;

namespace SMSystems.UI.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPatientService _patientService;

        public IndexModel(IInvoiceService invoiceService, IPatientService patientService)
        {
            _invoiceService = invoiceService;
            _patientService = patientService;
        }

        public IList<Invoice> Invoices { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? InvoiceID { get; set; }

        public async Task OnGetAsync()
        {

            var invoices = await _invoiceService.GetAll().ToListAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {

                var patients = await _patientService.GetAll().ToListAsync();

                patients = patients.Where(p => p.Name.Contains(SearchString, StringComparison.OrdinalIgnoreCase)).ToList();

                var patientIDs = patients.Select(p => p.ID).ToList();

                invoices = invoices.Where(inv => patientIDs.Contains(inv.PatientID)).ToList();
            }

            Invoices = invoices;
        }
    }
}
