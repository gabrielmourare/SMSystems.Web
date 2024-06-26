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
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _session;
        private readonly IMapper _mapper;

        public SessionService(ISessionRepository session, IMapper mapper)
        {
            _session = session;
            _mapper = mapper;
        }

        public async Task AddSessionAsync(Session session)
        {
            Session sessionMapped = _mapper.Map<Session>(session);
            await _session.SaveSessionAsync(sessionMapped);
        }

        public async Task DeleteSessionAsync(Session session)
        {           
            if (session == null)
            {
                throw new KeyNotFoundException("Session not found");
            }

            Session sessionMapped = _mapper.Map<Session>(session);
            await _session.DeleteSessionAsync(sessionMapped);
        }

        public IQueryable<Session> GetAll()
        {
            return _session.GetAllSessions();
        }

        public IQueryable<Session> GetAllPatientSessions(int patientId)
        {
            return _session.GetAllPatientSessions(patientId);
        }

        public async Task<Session?> GetSessionByIdAsync(int id)
        {
            return await _session.GetSessionByIdAsync(id);
        }

        public async Task UpdateSessionAsync(Session session)
        {
            Session sessionMapped = _mapper.Map<Session>(session);
            await _session.UpdateSessionAsync(sessionMapped);
        }

        public IQueryable<Session> GetAllInvoiceSessions(int invoiceId)
        {
            return _session.GetAllInvoiceSessions(invoiceId);
        }
    }
}
