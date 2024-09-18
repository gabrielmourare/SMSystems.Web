using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.DTOs
{
    public class InvoiceDetailsDTO
    {
        public string SessionValue { get; set; }
        public decimal TotalValue { get; set; }
        public string WrittenTotal { get; set; }
        public string EmissionDate { get; set; }
        public string City { get; set; }
        public string FullAddress { get; set; }
        public string Telephone { get; set; }
        public string ProfessionalName { get; set; }
        public string ProfessionalSocialNumber { get; set; }
        public string ProfessionalRCNumber { get; set; }
        public string Profession { get; set; }

        public string PatientName { get; set; }
        public string PatientSocialNumber { get; set; }
        public string Motivo { get; set; }
        public string PatientICD { get; set; }
        public DateTime PatientBirthDate { get; set; }


    }
}
