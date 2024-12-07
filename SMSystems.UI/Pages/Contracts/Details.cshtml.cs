using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Fluent;
using SMSystems.Application.DTOs;
using SMSystems.Application.Interfaces;
using SMSystems.Application.Printer.Interfaces;
using SMSystems.Application.Services;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Contracts
{
    public class DetailsModel : PageModel
    {
        private readonly IContractService _contractService;
        private readonly IPrinterService _reportService;

        public DetailsModel(IContractService contractService, IPrinterService reportService)
        {
            _contractService = contractService;
            _reportService = reportService;
        }

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
            ContractDetailsDTO invoiceDetails = await _contractService.GetContractDetails(id);

            // Sua lógica para gerar o PDF aqui
            var doc = _reportService.GeneratePDF(invoiceDetails);

            var pdf = doc.GeneratePdf();
            // Exemplo: retornar um arquivo PDF

            return File(pdf, "application/pdf", "document.pdf");
        }
    }
}
