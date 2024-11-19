using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;
using SMSystems.UI.Pages.Patients;
using System.ComponentModel.DataAnnotations;


namespace SMSystems.UI.Pages.Invoices
{
    public class IndexModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPatientService _patientService;

        public IndexModel(IInvoiceService invoiceService, IPatientService patientService)
        {
            _invoiceService = invoiceService;
            _patientService = patientService;
        }

        public IList<Invoice> Invoices { get; set; } = default!;

        public Invoice Invoice { get; set; }
        public Dictionary<int, string> PatientNames { get; set; } = new Dictionary<int, string>();


        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? InvoiceID { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SearchStatus { get; set; }

        [BindProperty]
        public List<int> SelectedIds { get; set; } = new List<int>();


        public async Task OnGetAsync()
        {
            // Inicializamos a consulta de faturas
            List<Invoice> invoiceQuery = await _invoiceService.GetAll();
            var patients = await _patientService.GetAll(); // Supondo que tenha um método assíncrono
            foreach (var patient in patients)
            {
                PatientNames[patient.ID] = patient.Name;
            }

            // Preencha o dropdown de Status
            ViewData["SearchStatus"] = new SelectList(new[]
                                                             {
                                                                new {Value = 0, Text = "Selecione um Status..."},
                                                                new { Value = 1, Text = "Enviado ao Paciente" },
                                                                new { Value = 2, Text = "Emitido" },
                                                                new { Value = 3, Text = "Aguardando Assinatura" },
                                                                new { Value = 4, Text = "Aguardando Emissão" }
                                                            }, "Value", "Text");

            // Filtro por nome de paciente (se fornecido)
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Filtra os pacientes com base na SearchString
                var filteredPatientIds = patients
                    .Where(p => p.Name.ToLower().Contains(SearchString.ToLower()))
                    .Select(p => p.ID)
                    .ToList(); // Cria uma lista com os IDs filtrados

                if (patients.Any())
                {
                    var query = invoiceQuery.Where(inv => filteredPatientIds.Contains(inv.PatientID)).ToList();
                    invoiceQuery = query;
                }
                else
                {
                    var query = invoiceQuery.Where(inv => false).ToList();
                    invoiceQuery = query;
                }
            }

            // Filtro por período (se as datas forem válidas)
            if (StartDate.HasValue && EndDate.HasValue)
            {
                var query = invoiceQuery.Where(inv => inv.EmissionDate >= StartDate.Value && inv.EmissionDate <= EndDate.Value).ToList();
                invoiceQuery = query;
            }
            else if (StartDate.HasValue) // Se só a data inicial for fornecida
            {
                var query = invoiceQuery.Where(inv => inv.EmissionDate >= StartDate.Value).ToList();
                invoiceQuery = query;
            }
            else if (EndDate.HasValue) // Se só a data final for fornecida
            {
                var query = invoiceQuery.Where(inv => inv.EmissionDate <= EndDate.Value).ToList();
                invoiceQuery = query;
            }

            // Filtro por status (se fornecido)
            if (SearchStatus.HasValue && SearchStatus.Value != 0)
            {
                var statusEnum = (InvoiceStatus)SearchStatus.Value; // Converte o valor int para o enum correspondente
                var query = invoiceQuery.Where(inv => inv.Status == statusEnum).ToList();
                invoiceQuery = query;
            }

            // Carregamos as faturas filtradas
            Invoices = invoiceQuery;
        }

        public string GetStatusColor(InvoiceStatus status)
        {
            return status switch
            {
                InvoiceStatus.Pending => "text-muted",  // Classe CSS para cor amarela               
                InvoiceStatus.Issued => "text-primary",  // Classe CSS para cor vermelha
                InvoiceStatus.Sent => "text-success", // Classe CSS para cor cinza
                _ => "text-dark"                         // Classe padrão
            };
        }

        public async Task<string> GetPatientNameAsync(int patientID)
        {
            var patient = await _patientService.GetPatientById(patientID);
            return patient?.Name ?? "Unknown"; // Retorna "Unknown" se o paciente não for encontrado
        }

        public IActionResult OnPostDeleteSelected()
        {
            if (SelectedIds == null || !SelectedIds.Any())
            {
                return RedirectToPage("./Index"); // Volta para a listagem se nada for selecionado
            }

            return RedirectToPage("/Invoices/BulkDelete", new { ids = string.Join(",", SelectedIds) });
        }
    }
}
