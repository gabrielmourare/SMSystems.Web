using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SMSystems.Application.DTOs;
using SMSystems.Domain.Entities;
using SMSystems.Printer.Interfaces;
using System.Text;


namespace SMSystems.Printer.Services
{
    public class PrinterService : IPrinterService
    {
        public byte[] GeneratePDF(InvoiceDetailsDTO invoiceDetails, List<Session> sessions)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = MontaReciboGeral(ms, invoiceDetails, sessions);

                byte[] result = ms.ToArray();

                return result;

            }
        }

        private Paragraph MontaHeader()
        {
            Paragraph p = new Paragraph();

            return p;
        }
        private Paragraph MontaParagrafo(string nomePaciente, string cpfPaciente, string valorExtenso, string motivo)
        {
            Paragraph p = new Paragraph();
            string dirFont = @"..\SMSystems.Printer\Resources\Fonts\centurygothic.ttf";
            PdfFont font = PdfFontFactory.CreateFont(dirFont);

            float fontSize = 11;
            // Texto para a primeira linha
            string line1StartText = "RECEBI DE ";
            string line1EndText = ", CPF ";
            string line1Text = line1StartText + nomePaciente + line1EndText + cpfPaciente;

            // Texto para a segunda linha
            string line2StartText = "A QUANTIA DE ";
            string line2Text = Environment.NewLine + line2StartText + valorExtenso;

            //Texto para a terceira linha

            string line3StartText = "Referente a: ";
            string line3Text = Environment.NewLine + line3StartText + motivo;

            // Define o tamanho máximo da linha em unidades de texto
            float maxLineWidth1 = 470; // Largura da linha desejada
            float maxLineWidth2 = 486;

            // Função para preencher a linha com sublinhados
            string FillLineWithUnderscores(string text, float maxWidth, PdfFont font, float fontSize)
            {
                float textWidth = font.GetWidth(text, fontSize);
                float underscoreWidth = font.GetWidth("_", fontSize);
                int underscoresNeeded = (int)((maxWidth - textWidth) / underscoreWidth);
                return text + new string('_', Math.Max(0, underscoresNeeded));
            }

            // Preenche as linhas com sublinhados
            string line1WithUnderscores = FillLineWithUnderscores(line1Text, maxLineWidth1, font, fontSize);
            string line2WithUnderscores = FillLineWithUnderscores(line2Text, maxLineWidth2, font, fontSize);
            

            p.Add(new Text(line1WithUnderscores));
            p.Add(new Text(line2WithUnderscores));
            p.Add(new Text(line3Text));

            return p;
        }

        private Paragraph MontaFooter(string cidade, string dataEmissao, string nomePsi, string cpfPsi, string endereco, string telefone)
        {
            StringBuilder footer = new StringBuilder();

            footer.Append(string.Format("{0} ", cidade));
            footer.Append(string.Format("{0}", dataEmissao));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("{0}", "__________________________________"));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("{0} CPF: {1}", nomePsi, cpfPsi));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("{0}", endereco));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("{0}", Environment.NewLine));
            footer.Append(string.Format("Contato: {0}", telefone));



            Paragraph p = new Paragraph(footer.ToString());
            p.SetTextAlignment(TextAlignment.CENTER);
            p.SetMarginTop(300);

            return p;
        }

        private Document MontaReciboGeral(MemoryStream ms, InvoiceDetailsDTO invoiceDetails, List<Session> sessions)
        {

            //Campos configuráveis; 

            //Patient Fields
            string nomePaciente = invoiceDetails.PatientName;
            string cpfPaciente = invoiceDetails.PatientSocialNumber;
            string CID = invoiceDetails.PatientICD;

            //Invoice Fields
            string vrRecibo = invoiceDetails.TotalValue;
            string valorExtenso = invoiceDetails.WrittenTotal;
            string vrSessao = invoiceDetails.SessionValue;
            string motivo = "Psicoterapia / Consultas com Psicóloga";//invoiceDetails.Motivo;


            //Company Fields/Professional Fields
            string cidade = invoiceDetails.City;
            string endereco = invoiceDetails.FullAddress;
            string telefone = invoiceDetails.Telephone;
            string nomePsi = invoiceDetails.ProfessionalName;
            string cpfPsi = invoiceDetails.ProfessionalSocialNumber;
            string crpPsi = invoiceDetails.ProfessionalRCNumber;
            string profissional = invoiceDetails.Profession;


            //Directories
            string dirLogo = @"..\SMSystems.Printer\Resources\Images\SMLogo.png";
            string dirFont = @"..\SMSystems.Printer\Resources\Fonts\centurygothic.ttf";


            //Date Field
            string dataEmissao = DateTime.Now.ToShortDateString();



            PdfWriter writer = new PdfWriter(ms);

            PdfDocument pdf = new PdfDocument(writer);
            PdfFont font = PdfFontFactory.CreateFont(dirFont);

            Document document = new Document(pdf, PageSize.A4);
            document.SetFont(font);

            Image logoImage = new Image(ImageDataFactory.Create(dirLogo)).SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Paragraph nomePsico = new Paragraph(nomePsi);
            Paragraph crp = new Paragraph(string.Format("{0} {1}", profissional, crpPsi));
            Paragraph recibo = new Paragraph(string.Format("RECIBO {0}", vrRecibo));
            Paragraph txtRecibo = MontaParagrafo(nomePaciente, cpfPaciente, valorExtenso, motivo);
            Paragraph sessoes = new Paragraph().Add(MontaParagrafoSessoes(sessions));
            Paragraph footer = MontaFooter(cidade, dataEmissao, nomePsi, cpfPsi, endereco, telefone);

            //ADICIONAR CID 
            //ADICIONAR NOME DO PACIENTE E DATA DE NASCIMENTO ANTES DAS SESSOES EXECUTADAS

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

            sessoes.SetPaddingBottom(0);
            sessoes.SetMarginBottom(0);

            footer.SetFixedPosition(150, 20, 300);
            footer.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            
                       
            document.Add(logoImage);
            document.Add(nomePsico);
            document.Add(crp);
            document.Add(recibo);
            document.Add(txtRecibo);
            
            document.Add(sessoes);
            document.Add(footer);

            document.Close();

            return document;
        }

        private iText.Layout.Element.Table MontaParagrafoSessoes(List<Session> sessions)
        {
            iText.Layout.Element.Table tb = new iText.Layout.Element.Table(new float[] { 1, 1 });

            // Adiciona cabeçalhos para as colunas

            tb.AddHeaderCell(new Cell().Add(new Paragraph("Consultas/Sessões Executadas:")).SetBorder(Border.NO_BORDER));
            tb.AddHeaderCell(new Cell().Add(new Paragraph(" ")).SetBorder(Border.NO_BORDER));

            foreach (Session session in sessions)
            {
                tb.AddCell(new Cell().Add(new Paragraph(session.Date.ToShortDateString())).SetBorder(Border.NO_BORDER));
                tb.AddCell(new Cell().Add(new Paragraph(session.Value.ToString("C"))).SetBorder(Border.NO_BORDER));

            }

            tb.SetBorder(Border.NO_BORDER);
            return tb;
        }
    }
}
