using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int ID { get; private set; }
        public decimal SessionValue { get; set; }
        public decimal TotalValue { get; set; }
        public List<Session>? Sessions { get; set; }
        public DateTime EmissionDate { get; set; }
        public int PatientID { get; set; }
    }
}
