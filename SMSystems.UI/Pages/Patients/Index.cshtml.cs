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

namespace SMSystems.UI.Pages.Patients
{
    public class IndexModel : PageModel
    {
        private readonly IPatientService _patientService;
        private readonly IContractService _contractService;

        public IndexModel(IPatientService patientService, IContractService contractService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _contractService = contractService ?? throw new ArgumentNullException(nameof(contractService));
        }

        [BindProperty]
        public IList<Patient> Patients { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool SearchByContract { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool SearchByActive { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? PatientName { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // Verifique se a conexão com o banco está OK antes de realizar o GetAll
                var patients = await _patientService.GetAll();
                var contracts = await _contractService.GetAll();


                if (!string.IsNullOrEmpty(SearchString))
                {
                    if (SearchByContract)
                    {
                        int contractID = 0;
                        contracts = contracts.Where(searchByContract => searchByContract.Name.ToLower().Contains(SearchString.ToLower())).ToList();

                        foreach (Contract contract in contracts)
                        {
                            contractID = contract.ID;
                        }

                        patients = patients.Where(search => search.ContractID == contractID).ToList();


                    }
                    else
                    {
                        patients = patients.Where(search => search.Name.ToLower().Contains(SearchString.ToLower())).ToList();

                    }
                }
                if (SearchByActive)
                {
                    patients = patients.Where(search => search.Active == true).ToList();

                }

                patients = patients.OrderBy(search => search.Name).ToList();
                Patients = patients;
            }
            catch (Exception ex)
            {
                // Log da exceção ou ação adequada (ex: exibir uma mensagem de erro)
                Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);

                // Opcionalmente, definir um valor de fallback para Patients (ex: lista vazia)
                Patients = new List<Patient>();

                // Aqui você também pode redirecionar para uma página de erro ou mostrar uma mensagem
                ModelState.AddModelError(string.Empty, "Não foi possível conectar ao banco de dados.");
            }
        }

    }
}
