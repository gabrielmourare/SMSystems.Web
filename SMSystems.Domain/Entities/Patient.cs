using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Patient
    {
        [Key]
        public int ID { get; set;}
        [DisplayName("Nome")]
        public string? Name { get; set; }
        [DisplayName("CPF")]
        public string? SocialNumber { get; set; }
        [DisplayName("WhatsApp")]
        public string? Phone { get; set; }
        public string? Email { get; set; }
        [DisplayName("Ativo?")]
        public bool Active { get; set; } = true;
        [DisplayName("Dt. Nascimento")]
        [DataType(DataType.Date)]       
        public DateTime BirthDate { get; set; }

        [DisplayName("CID")]
        public string? ICD { get; set; }
        public List<Invoice>? Invoices { get; set; }
        public List<Session>? Sessions { get; set; }
    }

    
}
