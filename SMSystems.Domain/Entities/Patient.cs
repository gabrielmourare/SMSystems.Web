using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Patient
    {
        public int ID { get; }
        public string? Name { get; set; }
        public string? SocialNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public bool Active { get; set; } = false;
        public DateTime BirthDate { get; set; }
        public string? ICD { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public List<Session>? Sessions { get; set; }
    }

    
}
