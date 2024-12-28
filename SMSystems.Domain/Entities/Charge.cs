using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Charge
    {
        [Key]
        public int ID { get; set; }

        [DisplayName("Valor Sessão")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal SessionValue { get; set; }

        [DisplayName("Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]

        public decimal TotalValue { get; set; }
        public List<ChargeSession>? ChargeSessions { get; set; }
        [DisplayName("Data Emissão")]
        [DataType(DataType.Date)]
        public DateTime EmissionDate { get; set; }

        [DisplayName("Paciente")]
        public int PatientID { get; set; }

        [DisplayName("Mensagem")]
        public string? Message { get; set; }

        public ChargeStatus Status { get; set; }

        public enum ChargeStatus
        {
            [Display(Name = "Aguardando Envio")]
            Pending = 1,
            [Display(Name = "Cobrança Enviada")]
            Sent = 2
        }
    }
}
