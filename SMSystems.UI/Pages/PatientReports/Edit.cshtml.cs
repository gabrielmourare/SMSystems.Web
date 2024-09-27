using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.PatientReports
{
    public class EditModel : PageModel
    {
        private readonly SMSystems.Data.SMSystemsDBContext _context;

        public EditModel(SMSystems.Data.SMSystemsDBContext context)
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

            var patientreport =  await _context.PatientReports.FirstOrDefaultAsync(m => m.ID == id);
            if (patientreport == null)
            {
                return NotFound();
            }
            PatientReport = patientreport;
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

            _context.Attach(PatientReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientReportExists(PatientReport.ID))
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

        private bool PatientReportExists(int id)
        {
            return _context.PatientReports.Any(e => e.ID == id);
        }
    }
}
