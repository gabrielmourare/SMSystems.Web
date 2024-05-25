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
    public class IndexModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;

        public IndexModel(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public IList<Invoice> Invoice { get; set; } = default!;

        public async Task OnGetAsync()
        {

            Invoice = await _invoiceService.GetAll().ToListAsync();

        }
    }
}
