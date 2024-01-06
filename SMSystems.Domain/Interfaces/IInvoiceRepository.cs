using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        public IQueryable<Invoice> GetAllInvoices();
        public Invoice GetInvoiceById(int id);
        public IQueryable<Invoice> GetInvoicesByPatientId(int patientId);
        public void AddInvoice(Invoice invoice);
        public void DeleteInvoice(int invoiceId);
        public void UpdateInvoice(int invoiceId);
        public IQueryable<Session> GetAllSessions(int patientId);
    }
}
