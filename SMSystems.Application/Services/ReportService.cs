using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSystems.Domain.Entities;
using SMSystems.Printer.Interfaces;

namespace SMSystems.Application.Services
{
    public class ReportService
    {
        private readonly IPrinterService _printerService;
        public ReportService(IPrinterService printerService)
        {
            _printerService = printerService;
        }

        public byte[] GenerateReport(int invoiceID)
        {
            return _printerService.PrintPDF(invoiceID);
        }
    }
}
