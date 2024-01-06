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

        public void AddSession(Session session)
        {
            _session.SaveSession(session);
        }

        public void DeleteSession(int id)
        {
           _session.DeleteSession(id);
        }

        public IQueryable<Session> GetAll()
        {
            return _session.GetAllSessions();
        }

        public IQueryable<Session> GetAllPatientSessions(int patientId)
        {
           return _session.GetAllPatientSessions(patientId);
        }

        public Session GetSessionById(int id)
        {
           return _session.GetSessionById(id);
        }

        public void UpdateSession(Session session)
        {
            _session.UpdateSession(session.ID);
        }
    }
}
