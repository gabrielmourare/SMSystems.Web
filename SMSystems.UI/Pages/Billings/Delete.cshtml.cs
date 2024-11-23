using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Billings
{
    public class DeleteModel : PageModel
    {
        private readonly SMSystems.Data.SMSystemsDBContext _context;

        public DeleteModel(SMSystems.Data.SMSystemsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Billing Billing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings.FirstOrDefaultAsync(m => m.ID == id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billing = await _context.Billings.FindAsync(id);
            if (billing != null)
            {
                Billing = billing;
                _context.Billings.Remove(Billing);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
