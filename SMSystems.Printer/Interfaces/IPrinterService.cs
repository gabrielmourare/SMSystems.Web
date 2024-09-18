﻿using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF;


using SMSystems.Application.DTOs;
using QuestPDF.Fluent;

namespace SMSystems.Printer.Interfaces
{
    public interface IPrinterService
    {
        public QuestPDF.Infrastructure.IDocument GeneratePDF(InvoiceDetailsDTO invoiceID, List<Session> sessions);
    }
}
