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
    public class IndexModel : PageModel
    {
        private readonly IBillingService _billingService;
        private readonly IPatientService _patientService;
        public Dictionary<int, string> PatientNames { get; set; } = new Dictionary<int, string>();

        public IndexModel(IBillingService billingService,IPatientService patientService)
        {
            _billingService = billingService;
            _patientService = patientService;
        }

        public IList<Billing> Billing { get;set; } = default!;

        public async Task OnGetAsync()
        {            
            var patients = await _patientService.GetAll(); // Supondo que tenha um método assíncrono
            foreach (var patient in patients)
            {
                PatientNames[patient.ID] = patient.Name;
            }
            Billing = await _billingService.GetAll();
        }
    }
}
