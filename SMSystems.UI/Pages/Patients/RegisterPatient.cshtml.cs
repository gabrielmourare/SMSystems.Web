using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using SMSystems.UI.ViewModels;



namespace SMSystems.UI.Pages.Patients
{
    public class RegisterPatientModel : PageModel
    {
        private IPatientService _patientService;
        private IMapper _mapper;

        [BindProperty]
        public PatientViewModel patientVM { get; set; }

        public List<string>? ModalMessages { get; set; }

        public RegisterPatientModel(IPatientService patientService, IMapper mapper)
        {
            _patientService = patientService;
            ModalMessages = new List<string>();
            _mapper = mapper;

        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            var errorMessage = string.Empty;

            if (!ModelState.IsValid)
            {
                return Page();
            }


            if (ModalMessages.Count > 0)
            {
                return Page();
            }

            Patient patient = _patientService.GetPatientBySN(patientVM.SocialNumber);

            if (patient != null)
            {
                if (patient.SocialNumber == patientVM.SocialNumber)
                {
                    ModalMessages.Add("Paciente com este CPF já existe.");
                    return Page();
                }
            }

            patientVM = new PatientViewModel
            {
                Email = patientVM.Email,
                Name = patientVM.Name,
                ICD = patientVM.ICD,
                SocialNumber = patientVM.SocialNumber,
                Phone = patientVM.Phone,
                BirthDate = patientVM.BirthDate

            };

            var patientMapped = _mapper.Map<Patient>(patientVM);

            _patientService.AddPatient(patientMapped);
            return Page();

        }
    }
}
