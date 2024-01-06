using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Data.Repositories
{
    internal class InvoiceRepository : IInvoiceRepository
    {
        public SMSystemsDBContext _context;

        public InvoiceRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public IQueryable<Invoice> GetAllInvoices()
        {
            return _context.Invoices;
        }

        public Invoice GetInvoiceById(int id)
        {
            return _context.Invoices.Find(id);
        }

        public IQueryable<Invoice> GetInvoicesByPatientId(int patientId)
        {
            return _context.Invoices.Where(invoice => invoice.PatientID == patientId);
        }

        public void AddInvoice(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }

        public void UpdateInvoice(int invoiceId)
        {
            _context.Invoices.Update(this.GetInvoiceById(invoiceId));
        }

        public void DeleteInvoice(int invoiceId)
        {
            _context.Remove(this.GetInvoiceById(invoiceId));
        }

        public IQueryable<Session> GetAllSessions(int patientId)
        {
            return _context.Sessions.Where(session => session.PatientID == patientId);
        }
    }
}
