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

        public IndexModel(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        [BindProperty]
        public IList<Patient> Patients { get; set; } = default!;

        [BindProperty(SupportsGet =true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet =true)]
        public string? PatientName { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // Verifique se a conexão com o banco está OK antes de realizar o GetAll
                var patients = await _patientService.GetAll().ToListAsync();

                if (!string.IsNullOrEmpty(SearchString))
                {
                    patients = patients.Where(search => search.Name.Contains(SearchString)).ToList();
                }

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
