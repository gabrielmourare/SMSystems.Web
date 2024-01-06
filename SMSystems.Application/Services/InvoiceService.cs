using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceService _invoice;        
        private readonly ISessionService _session;
        public IQueryable<Invoice> GetAll()
        {
            return _invoice.GetAll();
        }

        public IQueryable<Invoice> GetAllPatientInvoices(int patientId)
        {
            return _invoice.GetAllPatientInvoices(patientId);
        }

        public IQueryable<Session> GetAllInvoiceSessions(int patientId)
        {
            return _invoice.GetAllInvoiceSessions(patientId);
        }
        public Invoice GetInvoiceById(int invoiceId)
        {
            return _invoice.GetInvoiceById(invoiceId);
        }
        public void AddInvoice(Invoice invoice)
        {
            _invoice.AddInvoice(invoice);
            foreach(Session session in invoice.Sessions)
            {
                _session.AddSession(session);
            }
        }

        public void UpdateInvoice(int id)
        {
            _invoice.UpdateInvoice(id);
        }

        public void DeleteInvoice(int id)
        {
            _invoice.DeleteInvoice(id);
        }
    }
}
