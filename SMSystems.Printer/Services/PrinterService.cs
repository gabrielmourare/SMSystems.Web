using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SMSystems.Application.DTOs;
using SMSystems.Domain.Entities;
using SMSystems.Printer.Interfaces;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;



namespace SMSystems.Printer.Services
{
    public class PrinterService : IPrinterService
    {
        public QuestPDF.Infrastructure.IDocument GeneratePDF(InvoiceDetailsDTO invoiceDetails, List<Session> sessions)
        {
            return Document.Create(container =>
            {
                // Obter o caminho base, sem 'bin/Debug' ou 'bin/Release'
                string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // Navegar para o diretório raiz do projeto (duas pastas para trás, saindo de bin/Debug)
                string rootDirectory = Directory.GetParent(projectDirectory)?.Parent?.FullName;

                byte[] logoBytes = File.ReadAllBytes(Path.Combine(rootDirectory, @"..\..\Resources\Images\SMlogo.png"));

                string nomeProfissional = invoiceDetails.ProfessionalName;//"Andiara Sarraf Moura";
                string cpfProfissional = invoiceDetails.ProfessionalSocialNumber;// "066.171.616-37";
                string profissao = invoiceDetails.Profession;// "Psicóloga";
                string CRP = "CRP";
                string crpValue = invoiceDetails.ProfessionalRCNumber;
                string texto = string.Empty;
                string valor = invoiceDetails.TotalValue.ToString("C2");
                string cpfPaci = invoiceDetails.PatientSocialNumber;
                string nomePaci = invoiceDetails.PatientName;
                string quantia = invoiceDetails.WrittenTotal;
                string motivo = "Psicoterapia / Consultas com Psicóloga";
                string CID = invoiceDetails.PatientICD;
                DateTime dtNascPaci = invoiceDetails.PatientBirthDate;


                IContainer BlockStyle(IContainer container) => container.Padding(10);


                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));
                    page.DefaultTextStyle(x => x.FontFamily("Century Gothic"));

                    page.Header().Image(logoBytes);


                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(5);
                            x.Item().Text(nomeProfissional).AlignCenter().FontSize(12).ParagraphSpacing(10);
                            x.Item().Text(string.Format("{0} {1} {2}", profissao, CRP, crpValue)).AlignCenter().FontSize(12).ParagraphSpacing(10);

                            x.Item().Element(BlockStyle).Text(text =>
                            {
                                text.Span(string.Format("RECIBO ")).FontSize(21).Bold();
                                text.Span(string.Format("{0}", valor)).FontSize(21).Bold();
                                text.AlignCenter();

                            });

                            x.Item().Text(text =>
                            {
                                text.Span(string.Format("Recebi de "));
                                text.Span(string.Format("{0},", nomePaci)).Underline().Bold();
                                text.Span(string.Format("  CPF "));
                                text.Span(string.Format("{0},", cpfPaci)).Underline().Bold();
                                text.Justify();
                            });

                            x.Item().Text(text =>
                            {
                                text.Span(string.Format("A quantia de "));
                                text.Span(string.Format("{0},", quantia)).Underline().Bold();
                                text.Justify();
                            });

                            x.Item().Text(text =>
                            {
                                text.Span(string.Format("Referente à "));
                                text.Span(string.Format("{0}.", motivo)).Underline().Bold();
                                text.Justify();
                                text.EmptyLine();
                                text.EmptyLine();

                            });

                            x.Item().Text(text =>
                            {
                                text.Span(string.Format("CID 10 - H.D. "));
                                text.Span(string.Format("{0}", CID)).Underline().Bold();
                            });

                            x.Item().Text(text =>
                            {
                                text.Span(string.Format("Paciente: "));
                                text.Span(string.Format("{0}", nomePaci)).Underline().Bold();
                                text.Span(string.Format("  Data de Nasc.:"));
                                text.Span(string.Format("{0}", dtNascPaci.ToString("dd/MM/yyyy"))).Underline().Bold();
                                text.EmptyLine();
                                text.EmptyLine();

                            });

                            x.Item().Text(text =>
                            {
                                text.Span(string.Format("Consultas/Sessões Executadas: ")).Bold();

                            });

                            x.Item().Column(column =>
                            {
                                column.Item().Row(row =>
                                {
                                    row.RelativeItem(1).Border(1).Text("Data").AlignCenter().Bold();
                                    row.RelativeItem(2).Border(1).Text("Valor").AlignCenter().Bold();
                                });

                                foreach (var session in sessions)
                                {
                                    column.Item().Row(row =>
                                    {
                                        row.RelativeItem(1).Border(1).Text(session.Date.ToShortDateString()).AlignCenter();
                                        row.RelativeItem(2).Border(1).Text(session.Value.ToString("C2")).AlignCenter();
                                    });
                                }


                            });

                            x.Item().Text(text =>
                            {
                                text.EmptyLine();
                                text.EmptyLine();
                                text.EmptyLine();
                                text.EmptyLine();
                                text.Span("__________________________________").Underline();
                                text.EmptyLine();
                                text.Span(string.Format("{0}", nomeProfissional));
                                text.EmptyLine();
                                text.Span(string.Format("CPF: {0}", cpfProfissional));
                                text.EmptyLine();
                                text.EmptyLine();
                                text.Span("Santo André, São Paulo ");
                                text.Span(string.Format("{0}", DateTime.Now.ToShortDateString())).Underline().Bold();
                                text.AlignCenter();
                                text.EmptyLine();
                                text.Span("Contato (11) 984614824");
                            });




                        });





                });
            });
        }

    }
}
