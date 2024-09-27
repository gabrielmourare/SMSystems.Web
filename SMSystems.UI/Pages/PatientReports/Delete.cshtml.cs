using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.PatientReports
{
    public class DeleteModel : PageModel
    {
        private readonly SMSystems.Data.SMSystemsDBContext _context;

        public DeleteModel(SMSystems.Data.SMSystemsDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PatientReport PatientReport { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientreport = await _context.PatientReports.FirstOrDefaultAsync(m => m.ID == id);

            if (patientreport == null)
            {
                return NotFound();
            }
            else
            {
                PatientReport = patientreport;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientreport = await _context.PatientReports.FindAsync(id);
            if (patientreport != null)
            {
                PatientReport = patientreport;
                _context.PatientReports.Remove(PatientReport);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
