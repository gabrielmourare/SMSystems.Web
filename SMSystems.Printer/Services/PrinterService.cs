using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SMSystems.Domain.Entities;
using SMSystems.Printer.Interfaces;
using System.Text;


namespace SMSystems.Printer.Services
{
    public class PrinterService : IPrinterService
    {
        public byte[] PrintPDF(int invoiceID)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = MontaReciboGeral(ms);

                byte[] result = ms.ToArray();

                return result;

            }
        }

        private Paragraph MontaHeader()
        {
            Paragraph p = new Paragraph();

            return p;
        }
        private Paragraph MontaParagrafo(string nomePaciente, string cpfPaciente, string valorExtenso, string periodo)
        {
            StringBuilder stbuilder = new StringBuilder();

            stbuilder.AppendLine(string.Format("Recebi de {0}, CPF {1},", nomePaciente.PadRight(50, '_'), cpfPaciente.PadRight(26, '_')));
            stbuilder.AppendLine(string.Format("A quantia de {0},", valorExtenso.PadRight(80, '_')));
            stbuilder.AppendLine(string.Format("Referente à {0}.", periodo.PadRight(80, '_')));

            string finalText = stbuilder.ToString();

            Text text = new Text(finalText);

            text.SetTextAlignment(TextAlignment.JUSTIFIED);
            text.SetFontSize(11);

            return new Paragraph(text);
        }

        private Paragraph MontaFooter(string cidade, string dataEmissao)
        {
            Paragraph p =  new Paragraph(string.Format("{0} {1}", cidade, dataEmissao));
            p.SetTextAlignment(TextAlignment.CENTER);
            p.SetMarginTop(400);
            
            return p;
        }

        private Document MontaReciboGeral(MemoryStream ms)
        {
            decimal vrRecibo = 100;
            string nomePaciente = "Fulana de Tal";
            string cpfPaciente = "412.494.438-14";
            string valorExtenso = "Cento e oitenta reais";
            string periodo = "06/2024";
            string cidade = "São Caetano do Sul";
            string dataEmissao = DateTime.Now.ToShortDateString();



            PdfWriter writer = new PdfWriter(ms);

            PdfDocument pdf = new PdfDocument(writer);
            PdfFont font = PdfFontFactory.CreateFont(@"..\SMSystems.Printer\Resources\Fonts\centurygothic.ttf");

            Document document = new Document(pdf, PageSize.A4);
            document.SetFont(font);

            Image logoImage = new Image(ImageDataFactory.Create(@"..\SMSystems.Printer\Resources\Images\SMLogo.png")).SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Paragraph nomePsico = new Paragraph("Andiara Sarraf");
            Paragraph crp = new Paragraph("Psicóloga CRP 06/87574");
            Paragraph recibo = new Paragraph(string.Format("RECIBO {0}", vrRecibo.ToString("C2")));
            Paragraph txtRecibo = MontaParagrafo(nomePaciente, cpfPaciente, valorExtenso, periodo);
            Paragraph footer = MontaFooter(cidade, dataEmissao);



            logoImage.ScaleAbsolute((float)348.47, (float)119.43);

            nomePsico.SetTextAlignment(TextAlignment.CENTER);
            nomePsico.SetFontSize(12);
            nomePsico.SetMarginBottom(-10);

            crp.SetTextAlignment(TextAlignment.CENTER);
            crp.SetFontSize(12);
            crp.SetMarginBottom(-10);

            recibo.SetTextAlignment(TextAlignment.CENTER);
            recibo.SetFontSize(21);
            recibo.SetMarginBottom(0);


            document.Add(logoImage);
            document.Add(nomePsico);
            document.Add(crp);
            document.Add(recibo);
            document.Add(txtRecibo);
            document.Add(footer);

            document.Close();

            return document;
        }
    }
}
