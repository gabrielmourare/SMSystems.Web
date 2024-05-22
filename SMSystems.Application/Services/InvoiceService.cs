using AutoMapper;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoice;        
        private readonly ISessionRepository _session;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoice, ISessionRepository session, IMapper mapper)
        {
            _invoice = invoice;
            _session = session;
            _mapper = mapper;
        }

        public IQueryable<Invoice> GetAll()
        {
            return _invoice.GetAllInvoices();
        }

        public IQueryable<Invoice> GetAllPatientInvoices(int patientId)
        {
            return _invoice.GetInvoicesByPatientId(patientId);
        }

        public IQueryable<Session> GetAllInvoiceSessions(int patientId)
        {
            return _invoice.GetAllSessions(patientId);
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
                _session.SaveSession(session);
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
