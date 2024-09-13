using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SMSystems.Application.DTOs;

namespace SMSystems.Printer.Interfaces
{
    public interface IPrinterService
    {
        public byte[] GeneratePDF(InvoiceDetailsDTO invoiceID, List<Session> sessions);
    }
}
