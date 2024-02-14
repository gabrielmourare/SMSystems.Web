using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SMSystems.UI.ViewModels
{
    public class PatientViewModel 
    {
      
        public int ID { get; private set; }
        [BindProperty]
        [Required(ErrorMessage = "Preencha o campo Nome.")]
        public string? Name { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Preencha o campo CPF.")]
        [StringLength(11,ErrorMessage = "O CPF não pode ter mais do que 11 caracteres.")]
        public string? SocialNumber { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Preencha o campo Telefone.")]
        public string? Phone { get; set; }
        [BindProperty]
        [EmailAddress(ErrorMessage = "Email inválido.")]     
        public string? Email { get; set; }
        public bool Active { get; set; } = true;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public DateTime BirthDate { get; set; }
        public string? ICD { get; set; }
    }
}
