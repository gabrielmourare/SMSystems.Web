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
    public class DetailsModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IContractService _contractService;

        public DetailsModel(IPatientService patientService, IContractService contractService)
        {
            _patientService = patientService;
            _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        }

        public Patient Patient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            await PopulateContractsDropdownAsync();
            if (id == 0)
            {
                return NotFound();
            }

            var patient = await _patientService.GetPatientData(id);
            if (patient == null)
            {
                return NotFound();
            }
            else
            {
                Patient = patient;
            }
            return Page();
        }

        private async Task PopulateContractsDropdownAsync()
        {
            // Materializa os dados em uma lista
            var contracts = await _contractService.GetAll();

            // Aplica o Select na lista
            var contractList = contracts.Select(c => new
            {
                c.ID,
                DisplayName = $"{c.Name} - {c.SessionValue.ToString("C2")}"
            }).ToList();

            ViewData["ContractID"] = new SelectList(contractList, "ID", "DisplayName");
        }
    }
}
