using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Contract
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public decimal SessionValue { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }

    }
}
