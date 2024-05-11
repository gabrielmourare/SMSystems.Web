using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Patients
{
    public class EditPatientModel : PageModel
    {
        private readonly IPatientService _patientService;

        public EditPatientModel(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        public Patient Patient { get; set; }
        public void OnGet(int id)
        {
            Patient = _patientService.GetPatientById(id);
        }
    }
}
