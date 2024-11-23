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
    public class DeleteModel : PageModel
    {
        private readonly IContractService _contractService;

        public DeleteModel(IContractService contractService)
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
            else
            {
                Contract = contract;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var contract = await _contractService.GetContractById(id);
            if (contract != null)
            {
                Contract = contract;
                await _contractService.DeleteContract(Contract);

            }

            return RedirectToPage("./Index");
        }
    }
}
