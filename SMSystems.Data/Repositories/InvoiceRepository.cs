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

        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }

        public async Task<Invoice?> GetInvoiceByIdAsync(int id)
        {
            try
            {
                Invoice? invoice = await _context.Invoices
                                                 .Include(i => i.Sessions) // Inclui as sessões associadas à invoice
                                                 .FirstOrDefaultAsync(i => i.ID == id);
                return invoice;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }


        public async Task<List<Invoice>> GetInvoicesByPatientId(int patientId)
        {
            return await _context.Invoices.Include(invoice => invoice.Sessions).Where(invoice => invoice.PatientID == patientId).ToListAsync();
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

        public async Task<List<Session>> GetAllSessions(int patientId)
        {
            return await _context.Sessions.Where(session => session.PatientID == patientId).ToListAsync();
        }

        public async Task<bool> InvoiceExistsAsync(int id)
        {
            return await _context.Invoices.AnyAsync(e => e.ID == id);
        }

        public async Task<List<Invoice>> GetInvoicesByIdsAsync(List<int> ids)
        {
            return await _context.Invoices.Where(i => ids.Contains(i.ID)).ToListAsync();
        }

        public async Task DeleteInvoicesAsync(List<int> ids)
        {
            var invoices = await _context.Invoices.Where(i => ids.Contains(i.ID)).ToListAsync();
            _context.Invoices.RemoveRange(invoices);
            await _context.SaveChangesAsync();
        }

    }

}
