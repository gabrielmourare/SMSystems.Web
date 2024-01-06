using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Interfaces
{
    public interface IInvoiceService
    {
        public IQueryable<Invoice> GetAll();
        public Invoice GetInvoiceById(int id);
        public IQueryable<Invoice> GetAllPatientInvoices(int patientId);
        public IQueryable<Session> GetAllInvoiceSessions(int patientId);
        public void DeleteInvoice(int invoiceId);
        public void UpdateInvoice(int invoiceId);
        public void AddInvoice(Invoice invoice);
        


    }
}
