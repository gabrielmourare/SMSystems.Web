using iText.Layout;
using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Printer.Interfaces
{
    public interface IPrinterService
    {
        public byte[] PrintPDF(int invoiceID);
    }
}
