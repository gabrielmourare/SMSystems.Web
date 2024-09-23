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
        Task<List<Session?>> GetAllSessions();
        Task<List<Session?>> GetAllPatientSessions(int patientId);
        Task<Session?> GetSessionByIdAsync(int id);
        Task SaveSessionAsync(Session session);
        Task UpdateSessionAsync(Session session);
        Task DeleteSessionAsync(List<Session> session);
        Task<List<Session>> GetAllInvoiceSessions(int invoiceId);
    }
}
