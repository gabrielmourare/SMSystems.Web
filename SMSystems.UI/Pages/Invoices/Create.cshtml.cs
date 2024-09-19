using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using SMSystems.Application.Interfaces;
using SMSystems.Data;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.Pages.Invoices
{
    public class CreateModel : PageModel
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IPatientService _patientService;
        private readonly IContractService _contractService;


        public CreateModel(IInvoiceService invoiceService, IPatientService patientService, IContractService contractService)
        {
            _invoiceService = invoiceService;
            _patientService = patientService;
            _contractService = contractService;
        }

        public IActionResult OnGet()
        {
            PopulatePatientsDropdown();
            return Page();
        }

        [BindProperty]
        public Invoice Invoice { get; set; } = default!;

        [BindProperty]
        public List<DateTime> SessionDates { get; set; } = new List<DateTime>();


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Inicializa a lista de sessões, mas a cria apenas se for necessário
            Invoice.Sessions = new List<Session>();

            // Usa Task.WhenAll para buscar paciente e contrato em paralelo
            var patientTask = _patientService.GetPatientById(Invoice.PatientID);

            // Inicia a tarefa para obter o contrato somente se o paciente for encontrado
            var contractTask = patientTask.ContinueWith(t =>
            {
                if (t.Result != null)
                {
                    return _contractService.GetContractById(t.Result.ContractID);
                }
                return Task.FromResult<Contract>(null);
            }).Unwrap(); // Desembrulha a tarefa aninhada

            // Aguarda ambas as tarefas
            await Task.WhenAll(patientTask, contractTask);

            Patient patient = await patientTask;
            Contract contract = await contractTask;

            // Atualiza o valor da sessão de forma segura
            Invoice.SessionValue = contract?.SessionValue ?? 0;


            // Add the sessions to the invoice
            foreach (var date in SessionDates)
            {
                Invoice.Sessions.Add(new Session { Date = date, PatientID = Invoice.PatientID, Value = Invoice.SessionValue });
                Invoice.TotalValue += Invoice.SessionValue;
            }

            Invoice.EmissionDate = DateTime.Now.Date;


            await _invoiceService.AddInvoiceAsync(Invoice);


            return RedirectToPage("./Index");
        }

        private void PopulatePatientsDropdown()
        {
            var patients = _patientService.GetAll();
            ViewData["PatientID"] = new SelectList(patients, "ID", "Name");
        }
    }
}
