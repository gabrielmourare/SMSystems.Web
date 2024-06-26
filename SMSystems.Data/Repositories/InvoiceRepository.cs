using Microsoft.EntityFrameworkCore;
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
            Invoice? invoice = await _context.Invoices.FindAsync(id);

            List<Session> invoiceSessions = _context.Sessions.Where(sessions => sessions.InvoiceID == id).ToList();

            invoice.Sessions = invoiceSessions;

            return invoice;
        }

        public IQueryable<Invoice> GetInvoicesByPatientId(int patientId)
        {
            return _context.Invoices.Include(invoice => invoice.Sessions).Where(invoice => invoice.PatientID == patientId);
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            // Carrega a invoice existente incluindo as sessões associadas
            var existingInvoice = await _context.Invoices
                                                .Include(i => i.Sessions)
                                                .FirstOrDefaultAsync(i => i.ID == invoice.ID);

            if (existingInvoice == null)
            {
                throw new Exception("Invoice not found");
            }

            // Atualiza as propriedades da invoice
            _context.Entry(existingInvoice).CurrentValues.SetValues(invoice);

            // Atualiza as sessions
            UpdateSessions(existingInvoice, invoice);

            await _context.SaveChangesAsync();
        }

        private void UpdateSessions(Invoice existingInvoice, Invoice updatedInvoice)
        {
            // Mapeia as sessions existentes pelo ID
            var existingSessionsMap = existingInvoice.Sessions.ToDictionary(s => s.ID);

            // Remove as sessions que não estão na lista atualizada
            foreach (var existingSession in existingInvoice.Sessions.ToList())
            {
                if (!updatedInvoice.Sessions.Any(s => s.ID == existingSession.ID))
                {
                    _context.Sessions.Remove(existingSession);
                }
            }

            // Atualiza ou adiciona as sessions
            foreach (var updatedSession in updatedInvoice.Sessions)
            {
                if (existingSessionsMap.TryGetValue(updatedSession.ID, out var existingSession))
                {
                    // Atualiza a session existente
                    _context.Entry(existingSession).CurrentValues.SetValues(updatedSession);
                }
                else
                {
                    // Adiciona uma nova session
                    existingInvoice.Sessions.Add(updatedSession);
                }
            }
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

        public async Task<bool> InvoiceExistsAsync(int id)
        {
            return await _context.Patients.AnyAsync(e => e.ID == id);
        }
    }

}
