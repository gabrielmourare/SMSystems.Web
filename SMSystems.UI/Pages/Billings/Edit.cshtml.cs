using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Billings
{
    public class EditModel : PageModel
    {
        private readonly IBillingService _billingService;

        public EditModel(IBillingService billingService)
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

            var billing =  await _billingService.GetBillingById(id);
            if (billing == null)
            {
                return NotFound();
            }
            Billing = billing;
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

        

            try
            {
                await _billingService.UpdateBilling(Billing);
            }
            catch (DbUpdateConcurrencyException)
            {
                Task<bool> cobrancaExiste = _billingService.BillingExistsAsync(Billing.ID);
                bool result = await cobrancaExiste;

                if (!result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

      
    }
}
