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
    public class DetailsModel : PageModel
    {
        private readonly SMSystems.Data.SMSystemsDBContext _context;

        public DetailsModel(SMSystems.Data.SMSystemsDBContext context)
        {
            _context = context;
        }

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
    }
}
