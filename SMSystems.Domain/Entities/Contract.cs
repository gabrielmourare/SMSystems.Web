using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Entities
{
    public class Contract
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Descrição")]
        public string Name { get; set; }
        [DisplayName("Regras")]
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DisplayName("Valor Sessão")]
        public decimal SessionValue { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Inicio")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Validade")]
        public DateTime ExpirationDate { get; set; }
     

    }
}
