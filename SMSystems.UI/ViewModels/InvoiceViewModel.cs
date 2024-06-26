using Microsoft.AspNetCore.Mvc.Rendering;
using SMSystems.Domain.Entities;

namespace SMSystems.UI.ViewModels
{
    public class InvoiceViewModel
    {
        public int ID { get; set; }
        public decimal SessionValue { get; set; }
        public decimal TotalValue { get; set; }
        public List<Session>? Sessions { get; set; }
        public DateTime EmissionDate { get; set; }
        public int PatientID { get; set; }

        public int SelectedPatientId { get; set; }
        public IQueryable<SelectListItem> Patients { get; set; }
    }
}
