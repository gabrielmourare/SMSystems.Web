using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SMSystems.Application.DTOs;
using SMSystems.Domain.Entities;
using SMSystems.Printer.Interfaces;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
QuestPDF.Settings.License = LicenseType.Community;


namespace SMSystems.Printer.Services
{
    public class PrinterService : IPrinterService
    {
        public byte[] GeneratePDF(InvoiceDetailsDTO invoiceDetails, List<Session> sessions)
        {
            return new byte[0];
        }

    }
}
