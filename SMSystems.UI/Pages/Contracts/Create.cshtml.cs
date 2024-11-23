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

namespace SMSystems.UI.Pages.Contracts
{
    public class CreateModel : PageModel
    {
        private readonly IContractService _contractService;

        public CreateModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contract Contract { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _contractService.AddContract(Contract);
            
            return RedirectToPage("./Index");
        }
    }
}
