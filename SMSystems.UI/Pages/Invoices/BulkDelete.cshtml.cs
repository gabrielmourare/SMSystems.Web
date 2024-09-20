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

        // Método GET para carregar a página com os IDs selecionados
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

        // Método POST para realizar a exclusão após confirmação
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

            // Realiza a exclusão dos invoices com base nos IDs recebidos
            await _invoiceService.DeleteInvoicesAsync(SelectedIds);

            return RedirectToPage("./Index");
        }
    }


}
