using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface ISessionRepository
    {
        IQueryable<Session> GetAllSessions();
        IQueryable<Session> GetAllPatientSessions(int patientId);
        Task<Session?> GetSessionByIdAsync(int id);
        Task SaveSessionAsync(Session session);
        Task UpdateSessionAsync(Session session);
        Task DeleteSessionAsync(Session session);
        IQueryable<Session> GetAllInvoiceSessions(int invoiceId);
    }
}
