using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SMSystems.Application.DTOs;
using SMSystems.Domain.Entities;
using SMSystems.Application.Printer.Interfaces;
using System.Text;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using QuestPDF.Companion;



namespace SMSystems.Application.Printer.Services
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


        public QuestPDF.Infrastructure.IDocument GeneratePDF(ContractDetailsDTO contractDetails)
        {
            Document document = Document.Create(container =>
             {
                 // Obter o caminho base, sem 'bin/Debug' ou 'bin/Release'
                 string projectDirectory = AppDomain.CurrentDomain.BaseDirectory;

                 // Navegar para o diretório raiz do projeto (duas pastas para trás, saindo de bin/Debug)
                 string rootDirectory = Directory.GetParent(projectDirectory)?.Parent?.FullName;

                 byte[] logoBytes = File.ReadAllBytes(Path.Combine(rootDirectory, @"..\..\Resources\Images\SMlogo.png"));
                 byte[] signatureBytes = File.ReadAllBytes(Path.Combine(rootDirectory, @"..\..\Resources\Images\assinatura.png"));

                 string startDate = contractDetails.StartDate.ToString("MMMM").ToUpper();
                 string mesFormatado = char.ToUpper(startDate[0]) + startDate.Substring(1).ToLower();

                 string anoFormatado = contractDetails.StartDate.ToString("yyyy");

                 string expirationDate = contractDetails.ExpirationDate.ToShortDateString();
                 string sessionValue = contractDetails.SessionValue.ToString("C2");
                 string nomeProfissional = contractDetails.ProfessionalName;
                 string profissao = contractDetails.Profession;
                 string CRP = contractDetails.ProfessionalRCNumber;
                 string cpfProfissional = contractDetails.ProfessionalSocialNumber;




                 IContainer BlockStyle(IContainer container) => container.Padding(10);


                 container.Page(page =>
                 {
                     page.Size(PageSizes.A4);
                     page.Margin(1, Unit.Centimetre);
                     page.PageColor(Colors.White);
                     page.DefaultTextStyle(x => x.FontSize(20));
                     page.DefaultTextStyle(x => x.FontFamily("Century Gothic"));

                     page.Header().AlignCenter().Width(9.22f * 28.346f).Height(3.16f * 28.346f).Image(logoBytes);


                     page.Content()
                         .PaddingVertical(1, Unit.Centimetre)
                         .Column(x =>
                         {
                             x.Item().Element(BlockStyle).Text(text =>
                             {
                                 text.Span(string.Format("Contrato Psicoterapêutico")).FontSize(16);
                                 text.AlignCenter();

                             });

                             x.Item().Element(BlockStyle).Text(text =>
                             {
                                 text.Span(string.Format("Bem vindo à Sarraf Moura!")).FontSize(14);
                                 text.AlignCenter();
                             });

                             x.Item().Text(text =>
                             {
                                 text.Span(@"O processo psicoterapêutico é extremamente importante para a saúde mental e o crescimento pessoal;porém não existe prazo determinado para apresentação de resultados, cada pessoa tem sua resposta ao processo. Para que este processo ocorra de forma natural e satisfatória é necessário estabelecermos algumas diretrizes.");
                                 text.Justify();
                             });

                             x.Item().Text(text =>
                             {
                                 text.EmptyLine();
                                 text.Span("Sobre os Horários:").Bold();
                                 text.AlignLeft();

                             });


                             x.Item().Text(text =>
                             {
                                 text.Span(" • As sessões têm duração de ");
                                 text.Span("50 minutos").Bold();

                             });

                             x.Item().Text(text =>
                             {
                                 text.Span(" • Se houver atraso do cliente, esse tempo será descontado de sua sessão;");


                             });

                             x.Item().Text(text =>
                             {
                                 text.Span(" • Se houver atraso do terapeuta, esse tempo terá que ser reposto na sessão do cliente;");
                             });

                             x.Item().Text(text =>
                             {
                                 text.EmptyLine();
                                 text.Span("Sobre Cancelamentos:").Bold();
                                 text.AlignLeft();

                             });

                             x.Item().Text(text =>
                             {
                                 text.Span(" • O horário agendado é seu! Isso significa que não será utilizado enquanto estiver reservado para você; portanto é acordado que cancelamentos deverão ser informados com ");
                                 text.Span("24 horas de antecedência").Bold();
                                 text.AlignLeft();

                             });

                             x.Item().Text(text =>
                             {
                                 text.Span(" • Cancelamentos realizados com menos de 24 horas de antecedência são ");
                                 text.Span("cobrados normalmente").Bold();

                             });

                             x.Item().Text(text =>
                             {
                                 text.EmptyLine();
                                 text.Span("Sobre Pagamentos:").Bold();
                                 text.AlignLeft();

                             });

                             x.Item().Text(text =>
                             {
                                 text.Span(" • Os pagamentos das sessões devem ser efetuados ");
                                 text.Span("na data da sessão ou antecipadamente").Bold();

                             });

                             x.Item().Text(text =>
                             {
                                 text.Span(" • O valor é ajustado semestralmente nos meses de ");
                                 text.Span("Janeiro e Julho").Bold();

                             });

                             x.Item().Text(text =>
                             {
                                 text.AlignCenter();
                                 text.EmptyLine();
                                 text.Span(string.Format("O valor da sua sessão a partir de "));
                                 text.Span(string.Format("{0} de {1} ", mesFormatado, anoFormatado)).Bold();
                                 text.Span("será de ");
                                 text.Span(string.Format("{0}.", sessionValue)).Bold();
                                 text.EmptyLine();
                                 text.EmptyLine();
                                 text.EmptyLine();
                             });


                             x.Item().Column(column =>
                             {

                                 column.Item().AlignCenter().Width(123).Height(91).Image(signatureBytes);
                                 column.Item().Element(e =>
                                 {
                                     e.AlignCenter().Width(200).LineHorizontal(1); // Adiciona a linha horizontal corretamente aqui
                                 });

                                 column.Item().Text(text =>
                                 {
                                     text.EmptyLine();
                                     text.Span(string.Format("{0}", nomeProfissional));
                                     text.EmptyLine();
                                     text.Span(string.Format("CRP {0}", CRP));
                                     text.EmptyLine();
                                     text.EmptyLine();
                                     text.Span("Santo André, São Paulo ");
                                     text.Span(string.Format("{0}", DateTime.Now.ToShortDateString())).Underline().Bold();
                                     text.AlignCenter();
                                     text.EmptyLine();
                                     text.Span("@o.seu.desenvolvimento");
                                     text.EmptyLine();
                                     text.Span("Contato (11) 984614824");
                                 });
                             });
                         });
                 });
             });


            return document;
        }
    }
}
