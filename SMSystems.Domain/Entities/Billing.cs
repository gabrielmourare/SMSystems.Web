using System;
using System.Collections.Generic;
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
        public int PatientID { get; set; }
        public List<Session>? Sessions { get; set; }
        public decimal TotalValue { get; set; }
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
