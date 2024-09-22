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
    public class SessionRepository : ISessionRepository
    {
        private readonly SMSystemsDBContext _context;

        public SessionRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public async Task<List<Session>> GetAllSessions()
        {
            return await _context.Sessions.ToListAsync();
        }

        public async Task<List<Session>> GetAllPatientSessions(int patientId)
        {
            return await _context.Sessions.Where(session => session.PatientID == patientId).ToListAsync();
        }

        public async Task<Session?> GetSessionByIdAsync(int id)
        {
            return await _context.Sessions.FindAsync(id);
        }

        public async Task SaveSessionAsync(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSessionAsync(Session session)
        {
            _context.Sessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSessionAsync(List<Session> sessions)
        {
            foreach (Session session in sessions)
            {
                _context.Sessions.Remove(session);
            }
            await SaveAsync();
        }

        public Task<List<Session>> GetAllInvoiceSessions(int invoiceId)
        {
            return _context.Sessions.Where(session => session.InvoiceID == invoiceId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
