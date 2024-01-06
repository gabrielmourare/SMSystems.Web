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
        public IQueryable<Session> GetAllSessions();
        public IQueryable<Session> GetAllPatientSessions(int patientId);
        public Session? GetSessionById(int id);
        public void SaveSession(Session session);
        public void DeleteSession(int id);
        public void UpdateSession(int sessionID);
    }
}
