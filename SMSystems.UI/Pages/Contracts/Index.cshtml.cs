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

namespace SMSystems.UI.Pages.Contracts
{
    public class IndexModel : PageModel
    {
        private readonly IContractService _contractService;
        private readonly IPatientService _patientService;


        public IndexModel(IContractService contractService, IPatientService patientService)
        {
            _contractService = contractService;
            _patientService = patientService;
        }

        public IList<Contract> Contracts { get; set; } = default!;
        public IList<Patient> Patients { get; set; } = default!;

        [BindProperty]
        public string PatientIDS { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? ContractID { get; set; }

        
        public string? Message { get; set; }

        public async Task OnGetAsync()
        {
            await LoadPage();
        }

        public async Task LoadPage()
        {
            Contracts = await _contractService.GetAll();
            Patients = await _patientService.GetAll();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(PatientIDS))
            {
                return BadRequest("Nenhum ID foi recebido.");
            }

            string[] idsArray = PatientIDS.Split(',');
          
            List<string> patientIdsList = idsArray.ToList();

            foreach (string id in patientIdsList)
            {
                int idConvertido = Convert.ToInt32(id);
                int idContratoConvertido = Convert.ToInt32(ContractID);
                Patient patient = await _patientService.GetPatientById(idConvertido);

                patient.ContractID = idContratoConvertido;

                await _patientService.UpdatePatient(patient);

            }
            Message = "Contratos de pacientes atualizados com sucesso!";
            await LoadPage();
            return Page();
        }
    }
}
