using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Patients
{
    public class ListPatientsModel : PageModel
    {
        private readonly IPatientService _patientService;

        public ListPatientsModel(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        public IQueryable<Patient> Patients { get; set; }
        public void OnGet()
        {
            Patients = _patientService.GetAll();
        }
    }
}
