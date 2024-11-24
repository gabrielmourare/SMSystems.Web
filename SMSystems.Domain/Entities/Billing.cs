using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Billing
    {

        [Key]
        public int ID { get; set; }
        [DisplayName("Paciente")]
        public int PatientID { get; set; }
        [DisplayName("Sessões")]
        public List<Session>? Sessions { get; set; }
        [DisplayName("Total")]
        public decimal TotalValue { get; set; }
        [DisplayName("Mensagem")]
        public string? MessageContent { get; set; }
        public BillingStatus Status { get; set; }
        public enum BillingStatus
        {
            [Display(Name = "Aguardando Envio")]
            Pending = 0,
            [Display(Name = "Enviado")]
            Sent = 1
        }
    }
}
