using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class PatientReport
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string PatientId { get; set; }
        [Required]
        public DateTime ReportDate { get; set; }

        public string Content { get; set; }



    }
}
