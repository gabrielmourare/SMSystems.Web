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
        public SMSystemsDBContext _context;

        public SessionRepository(SMSystemsDBContext context)
        {
            _context = context;
        }
        public IQueryable<Session> GetAllSessions()
        {
            return _context.Sessions;
        }

        public Session? GetSessionById(int id)
        {
            return _context.Sessions.Find(id);
        }

        public void SaveSession(Session session)
        {
            _context.Sessions.Add(session);
        }

        public void UpdateSession(Session session)
        {
            _context.Sessions.Update(this.GetSessionById(session.ID));
        }

        public void DeleteSession(int id)
        {
            _context.Remove(this.GetSessionById(id));
        }

        public IQueryable<Session> GetAllPatientSessions(int patientId)
        {
            return _context.Sessions.Where(sessions => sessions.PatientID == patientId);
        }

        public void UpdateSession(int sessionID)
        {
           _context.Sessions.Update(GetSessionById(sessionID));
        }
    }
}
