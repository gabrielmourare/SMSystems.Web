using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Patient
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Nome")]
        [Required(ErrorMessage = "Preencha o nome!")]
        public string? Name { get; set; }
        [DisplayName("CPF")]
        [Required(ErrorMessage = "Preencha o CPF!")]
        public string? SocialNumber { get; set; }
        [DisplayName("WhatsApp")]
        [Required(ErrorMessage = "Preencha o WhatsApp!")]
        public string? Phone { get; set; }
        public string? Email { get; set; }
        [DisplayName("Ativo?")]
        public bool Active { get; set; } = true;
        [DisplayName("Dt. Nascimento")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Preencha a data de nascimento!")]
        public DateTime BirthDate { get; set; }

        [DisplayName("CID")]
        public string? ICD { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public List<Session>? Sessions { get; set; }
        [DisplayName("Contrato")]

        [Required(ErrorMessage = "Selecione um contrato!")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um contrato válido!")]
        public int ContractID { get; set; }
        [DisplayName("Envia mensagem de Aniversário?")]
        public bool SendBirthdayMessage { get; set; } = false;
        [DisplayName("Emite recibos?")]
        public bool GetsInvoice { get; set; } = false;
    }


}
