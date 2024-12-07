using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SMSystems.Application.DTOs;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using SMSystems.Application.Printer.Interfaces;
using QuestPDF.Fluent;

namespace SMSystems.UI.Pages
{
    public class SendToWhatsappModel : PageModel
    {
        private readonly IContractService _contractService;
        private readonly IPrinterService _reportService;

        public SendToWhatsappModel(IContractService contractService, IPrinterService reportService)
        {
            _contractService = contractService;
            _reportService = reportService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Obter detalhes e sess�es da fatura
            ContractDetailsDTO contractDetails = await _contractService.GetContractDetails(id);        

            // Gerar o PDF
            var doc = _reportService.GeneratePDF(contractDetails);
            var pdf = doc.GeneratePdf();

            // Salvar o PDF em uma pasta acess�vel
            string pdfFileName = $"document_{id}.pdf";
            string pdfPath = Path.Combine("wwwroot/temp", pdfFileName);
            System.IO.File.WriteAllBytes(pdfPath, pdf);

            // N�mero do WhatsApp para abrir a conversa
            string phoneNumber = "5511987654321"; // Substitua pelo n�mero do destinat�rio
            string whatsappUrl = $"https://wa.me/{phoneNumber}";

            // Armazenar as URLs no TempData
            TempData["PdfUrl"] = Url.Content($"~/temp/{pdfFileName}");
            TempData["WhatsappUrl"] = whatsappUrl;

            // Redirecionar para a p�gina que abrir� o WhatsApp Web
            return RedirectToPage("SendToWhatsapp");
        }
    }
}
