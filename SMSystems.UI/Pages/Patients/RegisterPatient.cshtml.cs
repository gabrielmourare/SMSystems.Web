using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Patients
{
    public class RegisterPatientModel : PageModel
    {
        private IPatientService _patientService;
        [BindProperty]
        public string ModalMessage { get; set; }

        public RegisterPatientModel(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public void OnGet()
        {
           
        }

        public void OnPost()
        {

            var emailAddress = Request.Form["patientEmail"];
            var name = Request.Form["patientName"];
            var ICD = Request.Form["patientICD"];
            var socialNumber = Request.Form["patientSocialNumber"];
            var phoneNumber = Request.Form["patientNumber"];
            var birthdate = Request.Form["patientBirthDate"];
            var errorMessage = string.Empty;
            ValidateFields(out errorMessage);

            if(!string.IsNullOrEmpty(errorMessage))
            {
                ModalMessage = errorMessage;
                return;
            }

            Patient patient = new Patient();

            patient.Name = name;
            patient.SocialNumber = socialNumber;
            patient.Email = emailAddress;
            patient.BirthDate = DateTime.Parse(birthdate);
            patient.Phone = phoneNumber;
            patient.ICD = ICD;
            patient.Active = true;

            if (_patientService.GetPatientBySN(socialNumber).SocialNumber == socialNumber)
            {
                ModalMessage="Paciente com este CPF já existe.";
                return;
            }

            _patientService.AddPatient(patient);

        }

        private void ValidateFields(out string? errorMessage)
        {
            
        }
    }
}
