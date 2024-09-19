using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Valor Sessão")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal SessionValue { get; set; }
        [DisplayName("Valor Total")]
        [DisplayFormat(DataFormatString = "{0:C2}")]

        public decimal TotalValue { get; set; }
        public List<Session>? Sessions { get; set; }
        [DisplayName("Data Emissão")]
        [DataType(DataType.Date)]
        public DateTime EmissionDate { get; set; }

        [DisplayName("Paciente")]
        public int PatientID { get; set; }

        [DisplayName("Emitido?")]
        public bool Issued { get; set; }
        [DisplayName("Status")]
        public InvoiceStatus Status { get; set; }
    }

    public enum InvoiceStatus
    {
        [Display(Name ="Aguardando Emissão")]
        Pending = 4,
        [Display(Name = "Aguardando Assinatura Digital")]
        WaitingSignature = 3,
        [Display(Name = "Aguardando Envio")]
        Issued = 2,
        [Display(Name = "Enviado ao Paciente")]
        Sent = 1
    }
}
