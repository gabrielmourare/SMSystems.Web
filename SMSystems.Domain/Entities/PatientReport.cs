using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("ID do Paciente")]
        public int PatientId { get; set; }
        [Required]
        [DisplayName("Data Prontuário")]
        [DataType(DataType.Date)]
        public DateTime ReportDate { get; set; }

        [DisplayName("Conteúdo/Evolução")]
        public string Content { get; set; }



    }
}
