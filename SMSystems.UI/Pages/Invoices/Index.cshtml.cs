using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;
using SMSystems.UI.Pages.Patients;

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
        public Dictionary<int, string> PatientNames { get; set; } = new Dictionary<int, string>();


        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? InvoiceID { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }


        public async Task OnGetAsync()
        {
            // Inicializamos a consulta de faturas
            IQueryable<Invoice> invoiceQuery = _invoiceService.GetAll();
            var patients = await _patientService.GetAll(); // Supondo que tenha um método assíncrono
            foreach (var patient in patients)
            {
                PatientNames[patient.ID] = patient.Name;
            }

            // Filtro por nome de paciente (se fornecido)
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Filtra os pacientes com base na SearchString
                var filteredPatientIds = patients
                    .Where(p => p.Name.Contains(SearchString))
                    .Select(p => p.ID)
                    .ToList(); // Cria uma lista com os IDs filtrados

                if (patients.Any())
                {
                    invoiceQuery = invoiceQuery.Where(inv => filteredPatientIds.Contains(inv.PatientID));
                }
                else
                {
                    invoiceQuery = invoiceQuery.Where(inv => false);
                }
            }

            // Filtro por período (se as datas forem válidas)
            if (StartDate.HasValue && EndDate.HasValue)
            {
                invoiceQuery = invoiceQuery.Where(inv => inv.EmissionDate >= StartDate.Value && inv.EmissionDate <= EndDate.Value);
            }
            else if (StartDate.HasValue) // Se só a data inicial for fornecida
            {
                invoiceQuery = invoiceQuery.Where(inv => inv.EmissionDate >= StartDate.Value);
            }
            else if (EndDate.HasValue) // Se só a data final for fornecida
            {
                invoiceQuery = invoiceQuery.Where(inv => inv.EmissionDate <= EndDate.Value);
            }

            // Carregamos as faturas filtradas
            Invoices = await invoiceQuery.ToListAsync();
        }

        public string GetStatusColor(InvoiceStatus status)
        {
            return status switch
            {
                InvoiceStatus.Pending => "text-muted",  // Classe CSS para cor amarela
                InvoiceStatus.WaitingSignature => "text-info",    // Classe CSS para cor verde
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
    }
}
