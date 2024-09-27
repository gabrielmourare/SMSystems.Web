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
    public class DetailsModel : PageModel
    {
        private readonly SMSystems.Data.SMSystemsDBContext _context;

        public DetailsModel(SMSystems.Data.SMSystemsDBContext context)
        {
            _context = context;
        }

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
    }
}
