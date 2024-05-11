using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Domain.Entities;
using SMSystems.UI.ViewModels;
using SMSystems.UI.ViewModels.Pager;

namespace SMSystems.UI.Pages.Patients
{
    public class ListPatientsModel : PageModel
    {
        private readonly IPatientService _patientService;


        public ListPatientsModel(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        public IQueryable<PatientViewModel> Patients { get; set; }
        public void OnGet(int pg = 1)
        {

            try
            {
                IQueryable<Patient> patients = _patientService.GetAll();

                
                const int pageSize = 4;
                if (pg < 1)
                {
                    pg = 1;
                }

                int recsCount = patients.Count();

                var pager = new Pager(recsCount, pg, pageSize);

                int recSkip = (pg - 1) * pageSize;

                IQueryable<PatientViewModel> data = patients
           .Skip(recSkip)
           .Take(pager.PageSize)
           .Select(p => new PatientViewModel
           {   
               ID = p.ID,
               Name = p.Name,
               SocialNumber = p.SocialNumber,
               Phone = p.Phone,
               Email = p.Email,
               Active = p.Active,
               BirthDate = p.BirthDate,
               ICD = p.ICD
           });

                ViewData["PagerPatients"] = pager;

                Patients = data;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
            }

        }
    }
}
