using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.DTOs
{
    public class ContractDetailsDTO
    {
        public decimal SessionValue { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
        public string ProfessionalName { get; set; }
        public string ProfessionalSocialNumber { get; set; }
        public string ProfessionalRCNumber { get; set; }
        public string Profession { get; set; }

    }
}
