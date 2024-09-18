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

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? InvoiceID { get; set; }

        public async Task OnGetAsync()
        {
            // Inicializamos a consulta de faturas
            IQueryable<Invoice> invoiceQuery = _invoiceService.GetAll();

            // Se há uma string de busca, aplicamos o filtro
            if (!string.IsNullOrEmpty(SearchString))
            {
                // Buscamos os pacientes que correspondem ao nome digitado (case-insensitive por padrão no SQL Server)
                var patients = await _patientService
                    .GetAll()
                    .Where(p => p.Name.Contains(SearchString)) // Usamos o Contains simples
                    .Select(p => p.ID) // Seleciona apenas os IDs dos pacientes
                    .ToListAsync();

                // Verificamos se há pacientes que correspondem à busca
                if (patients.Any())
                {
                    // Filtramos as faturas para incluir apenas as que pertencem aos pacientes encontrados
                    invoiceQuery = invoiceQuery.Where(inv => patients.Contains(inv.PatientID));
                }
                else
                {
                    // Se nenhum paciente foi encontrado, retornamos uma lista vazia de faturas
                    invoiceQuery = invoiceQuery.Where(inv => false); // Nenhum resultado
                }
            }

            // Por fim, carregamos as faturas após aplicar o filtro
            Invoices = await invoiceQuery.ToListAsync();
        }

    }
}
