using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Billings
{
    public class CreateModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IContractService _contractService;
        private readonly IBillingService _billingService;

        public CreateModel(IPatientService patientService, IContractService contractService, IBillingService billingService)
        {
            _billingService = billingService;
            _contractService = contractService;
            _patientService = patientService;
        }

        [BindProperty]
        public List<DateTime> SessionDates { get; set; } = new List<DateTime>();

       

        public async Task<IActionResult> OnGet()
        {
            await PopulatePatientsDropdown();
            return Page();
        }

        [BindProperty]
        public Billing Billing { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Billing.Sessions = new List<Session>();

            // Usa Task.WhenAll para buscar paciente e contrato em paralelo
            var patientTask = _patientService.GetPatientById(Billing.PatientID);

            // Inicia a tarefa para obter o contrato somente se o paciente for encontrado
            var contractTask = patientTask.ContinueWith(t =>
            {
                if (t.Result != null)
                {
                    return _contractService.GetContractById(t.Result.ContractID);
                }
                return Task.FromResult<Contract>(null);
            }).Unwrap(); // Desembrulha a tarefa aninhada

            // Aguarda ambas as tarefas
            await Task.WhenAll(patientTask, contractTask);

            Patient patient = await patientTask;
            Contract contract = await contractTask;

            decimal valorContrato = 0;


            decimal valorPersonalizado = 0;


            decimal valorFinal = 0;


            return RedirectToPage("./Index");
        }
        private async Task PopulatePatientsDropdown()
        {
            var patients = await _patientService.GetAll();
            ViewData["PatientID"] = new SelectList(patients, "ID", "Name");
        }
    }
}
