using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.Services;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages
{
    public class ListPatientsModel : PageModel
    {
        private readonly PatientService _patientService;
        
        public IQueryable<Patient> Patients { get; set; }
        public void OnGet()
        {
            Patients = _patientService.GetAll();
        }
    }
}
