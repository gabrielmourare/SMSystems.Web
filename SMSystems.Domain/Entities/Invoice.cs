using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Invoice
    {
        protected int ID { get; set; }
        public decimal SessionValue { get; set; }
        public decimal TotalValue { get; set; }
        public List<Session>? Sessions { get; set; }
        public DateTime EmissionDate { get; set; }
        public int PatientID { get; set; }
    }
}
