using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Patients
{
    public class CreateModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IContractService _contractService;
        public CreateModel(IPatientService patientService, IContractService contractService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateContractsDropdownAsync();

            Patient = new Patient
            {
                Active = true // Definido como marcado por padrão
            };

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                await PopulateContractsDropdownAsync();
                return Page();
            }

            // Verifique se o método de atualização realmente salva as mudanças
            await _patientService.AddPatient(Patient);

            return RedirectToPage("./Index");
        }

        async Task<bool> PatientExists(int id)
        {
            return (await _patientService.PatientExistsAsync(id));
        }

        private async Task PopulateContractsDropdownAsync()
        {
            var contracts = await _contractService.GetAll();

            var contractList = new List<SelectListItem>
    {
        new SelectListItem { Value = "0", Text = "Selecione um contrato" } // Opção padrão
    };

            if (contracts != null)
            {
                contractList.AddRange(contracts.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = $"{c.Name} - {c.SessionValue.ToString("C2")}"
                }));
            }

            ViewData["ContractID"] = contractList;
        }
    }
}
