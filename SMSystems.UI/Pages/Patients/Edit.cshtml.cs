using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IContractService _contractService;
        public EditModel(IPatientService patientService, IContractService contractService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        }

        [BindProperty]
        public Patient Patient { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            await PopulateContractsDropdownAsync();
            if (id == null)
            {
                return NotFound();
            }

            var patient = _patientService.GetPatientById(id.Value);
            if (patient == null)
            {
                return NotFound();
            }
            Patient = await patient;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Verifique se o método de atualização realmente salva as mudanças
                await _patientService.UpdatePatient(Patient);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PatientExists(Patient.ID))
                {
                    return NotFound();
                }
                else
                {
                    // Log para debug: verificar o erro de concorrência
                    // Por exemplo, Log.Error("Concurrency issue occurred while updating patient: ", ex);
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        async Task<bool> PatientExists(int id)
        {
            return (await _patientService.PatientExistsAsync(id));
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
