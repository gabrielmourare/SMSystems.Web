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

namespace SMSystems.UI.Pages.Invoices
{
    public class DeleteModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ISessionService _sessionService;

        public DeleteModel(IInvoiceService invoiceService, ISessionService sessionService)
        {
            _invoiceService = invoiceService;
            _sessionService = sessionService;
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }
            else
            {
                Invoice = invoice;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);

            List<Session> sessions = await _sessionService.GetAllInvoiceSessions(id);


            await _sessionService.DeleteSessionAsync(sessions.ToList());



            if (invoice != null)
            {
                Invoice = invoice;

                await _invoiceService.DeleteInvoiceAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
