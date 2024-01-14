using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Patient
    {
        [Key]
        public int ID { get; private set;}
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
