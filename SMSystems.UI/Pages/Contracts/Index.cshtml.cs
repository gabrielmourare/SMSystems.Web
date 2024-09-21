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

        public IndexModel(IContractService contractService)
        {
            _contractService = contractService;
        }

        public IList<Contract> Contracts { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? ContractID { get; set; }

        public async Task OnGetAsync()
        {
           Contracts = await _contractService.GetAll();
        }
    }
}
