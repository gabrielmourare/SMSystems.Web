﻿using iText.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Printer.Interfaces
{
    public interface IPrinter
    {
        public byte[] PrintPDF();
    }
}
