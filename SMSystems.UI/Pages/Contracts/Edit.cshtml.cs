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

namespace SMSystems.UI.Pages.Contracts
{
    public class EditModel : PageModel
    {
        private readonly IContractService _contractService;

        public EditModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        [BindProperty]
        public Contract Contract { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var contract = await _contractService.GetContractById(id);
            if (contract == null)
            {
                return NotFound();
            }
            Contract = contract;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _contractService.UpdateContract(Contract);
            }
            catch (DbUpdateConcurrencyException)
            {
                Task<bool> verificaContratoExistente = _contractService.ContractExistsAsync(Contract.ID);
                bool result = await verificaContratoExistente;

                if (!result)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
