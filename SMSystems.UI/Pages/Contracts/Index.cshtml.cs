using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Contracts
{
    public class IndexModel : PageModel
    {
        private readonly SMSystems.Data.SMSystemsDBContext _context;

        public IndexModel(SMSystems.Data.SMSystemsDBContext context)
        {
            _context = context;
        }

        public IList<Contract> Contract { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Contract = await _context.Contracts.ToListAsync();
        }
    }
}
