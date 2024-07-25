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
namespace SMSystems.UI.MVC.Controllers.Patients
{
    public class PatientHomeController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientHomeController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        [BindProperty]
        public IList<Patient> Patients { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? PatientName { get; set; }

        public async Task<IActionResult>Index(string? searchString)
        {
            var patients = await _patientService.GetAll().ToListAsync();

            if (!string.IsNullOrEmpty(SearchString))
            {
                patients = patients.Where(search => search.Name.Contains(SearchString)).ToList();
            }

            var viewModel = new PatientViewModel
            {
                Patients = patients,
                SearchString = searchString
            };

            return View(viewModel);

        }
    }
}
