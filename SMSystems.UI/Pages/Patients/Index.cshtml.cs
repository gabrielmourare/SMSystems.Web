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

namespace SMSystems.UI.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly IPatientService _patientService;

        public IndexModel(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        [BindProperty]
        public IList<Patient> Patient { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Patient = await _patientService.GetAll().ToListAsync();

        }
    }
}
