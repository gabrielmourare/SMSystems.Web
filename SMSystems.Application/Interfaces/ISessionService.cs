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
        Task DeleteSessionAsync(List<Session> session);
        Task<List<Session>> GetAll();
        Task<List<Session>> GetAllPatientSessions(int patientId);
        Task<Session?> GetSessionByIdAsync(int id);
        Task UpdateSessionAsync(Session session);

        Task<List<Session>> GetAllInvoiceSessions(int invoiceId);
    }
}
