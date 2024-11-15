using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Invoices
{
    public class EditModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPatientService _patientService;
        private readonly ISessionService _sessionService;
        private readonly IContractService _contractService;

        public EditModel(IInvoiceService invoiceService, IPatientService patientService, ISessionService sessionService, IContractService contractService)
        {
            _invoiceService = invoiceService;
            _patientService = patientService;
            _sessionService = sessionService;
            _contractService = contractService;
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;
        public List<Session> Sessions { get; set; } = new List<Session>();

        [BindProperty]
        public List<DateTime> SessionDates { get; set; } = new List<DateTime>();

     
        public string Error { get; set; } = string.Empty;

        [BindProperty]
        public bool EditaValorSessao { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await PopulatePatientsDropdown();
            if (id == 0)
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            List<Session> sessions = await _sessionService.GetAllInvoiceSessions(id);

            SessionDates = sessions.Select(s => s.Date).ToList();

            invoice.Sessions = sessions;

            Sessions = sessions;

            Invoice = invoice;

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
           
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", new { id = Invoice.ID });
            }

            Patient patient = await _patientService.GetPatientById(Invoice.PatientID);

            Contract contract = await _contractService.GetContractById(patient.ContractID);


            Invoice.TotalValue = 0;
            foreach (var sessionDate in SessionDates)
            {
                decimal valorFinal = 0;

                if (EditaValorSessao)
                {
                    valorFinal = Invoice.SessionValue;
                } else
                {
                    valorFinal = contract != null ? contract.SessionValue : 0;
                }

                Invoice.SessionValue = valorFinal;

                Session session = new Session()
                {
                    Date = sessionDate,
                    InvoiceID = Invoice.ID,
                    PatientID = Invoice.PatientID,
                    Value = Invoice.SessionValue
                };
                Sessions.Add(session);

                Invoice.TotalValue += Invoice.SessionValue;
            }

            // Atribuir as sessões preparadas à fatura
            Invoice.Sessions = Sessions;
            Invoice.Status = 0;

            // Chamar o serviço para atualizar a fatura e as sessões associadas
            await _invoiceService.UpdateInvoiceAsync(Invoice);

            return RedirectToPage("./Index");
        }

        private async Task<bool> InvoiceExists(int id)
        {
            return (await _invoiceService.InvoiceExistsAsync(id));
        }

        private async Task PopulatePatientsDropdown() // Agora o retorno é Task
        {
            var patients = await _patientService.GetAll();
            ViewData["PatientID"] = new SelectList(patients, "ID", "Name");
        }
    }
}
