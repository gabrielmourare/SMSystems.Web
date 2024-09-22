using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using SMSystems.Application.DTOs;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Data;
using SMSystems.Domain.Entities;
using SMSystems.Printer;
using SMSystems.Printer.Interfaces;
using SMSystems.Printer.Services;


namespace SMSystems.UI.Pages.Invoices
{
    public class DetailsModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ISessionService _sessionService;
        private readonly IPrinterService _reportService;


        public DetailsModel(IInvoiceService invoiceService, ISessionService sessionservice, IPrinterService reportService)
        {
            _invoiceService = invoiceService;
            _sessionService = sessionservice;
            _reportService = reportService;
        }

        public Invoice Invoice { get; set; } = default!;
        public List<Session> Sessions { get; set; } = new List<Session>();

        [BindProperty]
        public List<DateTime> SessionDates { get; set; } = new List<DateTime>();

        public async Task<IActionResult> OnGetAsync(int id)
        {

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

            invoice.Sessions = sessions;

            Sessions = sessions;

            Invoice = invoice;

            return Page();
        }


       public async Task<IActionResult> OnPostAsync(int id)
        {
            InvoiceDetailsDTO invoiceDetails = await _invoiceService.GetInvoiceDetails(id);

            List<Session> sessions = await _sessionService.GetAllInvoiceSessions(id);

            // Sua lógica para gerar o PDF aqui
            var doc = _reportService.GeneratePDF(invoiceDetails, sessions);

            var pdf = doc.GeneratePdf();
            // Exemplo: retornar um arquivo PDF

            return File(pdf, "application/pdf", "document.pdf");
        }
       
    }
}
