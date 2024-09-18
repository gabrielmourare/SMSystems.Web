using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using QuestPDF.Previewer;
namespace SMSystems.Printer
{
    internal class PrinterPreviewer
    {
        public class PdfPrinter
        {
            public static void Main(string[] args)
            {
                ShowPreview();
            }

            public static void ShowPreview()
            {
                Console.WriteLine("Iniciando o QuestPDF Previewer na camada Printer...");

                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Content()
                            .Padding(50)
                            .Background("#FFF")
                            .Column(column =>
                            {
                                column.Item().Text("!");
                            });
                    });
                });

                document.ShowInPreviewer();
            }
        }
    }
}
