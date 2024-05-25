using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Session
    {
        [Key]
        public int ID { get; set; }
        public int PatientID { get; set; }
        public int InvoiceID { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }


    }
}
