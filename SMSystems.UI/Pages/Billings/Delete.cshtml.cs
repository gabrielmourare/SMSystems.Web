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

namespace SMSystems.UI.Pages.Billings
{
    public class DeleteModel : PageModel
    {
        private readonly IBillingService _billingService;

        public DeleteModel(IBillingService billingService)
        {
            _billingService = billingService;
        }

        [BindProperty]
        public Billing Billing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var billing = await _billingService.GetBillingById(id);

            if (billing == null)
            {
                return NotFound();
            }
            else
            {
                Billing = billing;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var billing = await _billingService.GetBillingById(id);
            if (billing != null)
            {
                Billing = billing;
                await _billingService.DeleteBilling(Billing);

            }

            return RedirectToPage("./Index");
        }
    }
}
