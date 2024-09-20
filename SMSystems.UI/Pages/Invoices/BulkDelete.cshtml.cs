using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Invoices
{
    public class BulkDeleteModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly ISessionService _sessionService;

        [BindProperty]
        public List<Invoice> Invoices { get; set; }

        [BindProperty]
        public List<int> SelectedIds { get; set; }

        public BulkDeleteModel(IInvoiceService invoiceService, ISessionService sessionService)
        {
            _invoiceService = invoiceService;
            _sessionService = sessionService;
        }

        // M�todo GET para carregar a p�gina com os IDs selecionados
        public async Task<IActionResult> OnGetAsync(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return NotFound();
            }

            SelectedIds = ids.Split(',').Select(int.Parse).ToList();
            Invoices = await _invoiceService.GetInvoicesByIdsAsync(SelectedIds);

            return Page();
        }

        // M�todo POST para realizar a exclus�o ap�s confirma��o
        public async Task<IActionResult> OnPostAsync()
        {
            if (SelectedIds == null || !SelectedIds.Any())
            {
                return RedirectToPage("./Index");
            }

            foreach (int id in SelectedIds)
            {
                IQueryable<Session> sessions = _sessionService.GetAllInvoiceSessions(id);
                await _sessionService.DeleteSessionAsync(sessions.ToList());
            }

            // Realiza a exclus�o dos invoices com base nos IDs recebidos
            await _invoiceService.DeleteInvoicesAsync(SelectedIds);

            return RedirectToPage("./Index");
        }
    }


}
