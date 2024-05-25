using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly SMSystemsDBContext _context;

        public InvoiceRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public IQueryable<Invoice> GetAllInvoices()
        {
            return _context.Invoices;
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            return await _context.Invoices.FindAsync(id);
        }

        public IQueryable<Invoice> GetInvoicesByPatientId(int patientId)
        {
            return _context.Invoices.Where(invoice => invoice.PatientID == patientId);
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceAsync(Invoice invoice)
        {
            _context.Remove(invoice);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Session> GetAllSessions(int patientId)
        {
            return _context.Sessions.Where(session => session.PatientID == patientId);
        }
    }

}
