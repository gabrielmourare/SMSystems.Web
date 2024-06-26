using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Interfaces
{
    public interface ISessionService
    {
        Task AddSessionAsync(Session session);
        Task DeleteSessionAsync(Session session);
        IQueryable<Session> GetAll();
        IQueryable<Session> GetAllPatientSessions(int patientId);
        Task<Session?> GetSessionByIdAsync(int id);
        Task UpdateSessionAsync(Session session);

        IQueryable<Session> GetAllInvoiceSessions(int invoiceId);
    }
}
