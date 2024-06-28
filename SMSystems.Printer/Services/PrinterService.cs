using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SMSystems.Printer.Interfaces;


namespace SMSystems.Printer.Services
{
    public class PrinterService : IPrinter
    {       
        public byte[] PrintPDF()
        {
            using (MemoryStream ms = new MemoryStream())
            {              

                PdfWriter writer = new PdfWriter(ms);

                PdfDocument pdf = new PdfDocument(writer);

                Document document = new Document(pdf);

                Paragraph header = new Paragraph("TESTE IMPRESSÃO").SetTextAlignment(TextAlignment.CENTER);

                document.Add(header);
                document.Close();
                byte[] result = ms.ToArray();

                return result;

            }
            


        }
    }
}
