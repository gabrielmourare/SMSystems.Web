using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Session
    {
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int InvoiceID { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }


    }
}
