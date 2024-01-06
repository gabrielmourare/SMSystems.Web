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
        public IQueryable<Session> GetAll();
        public IQueryable<Session> GetAllPatientSessions(int patientId);
        public Session GetSessionById(int id);
        
        public void UpdateSession(Session sessionId);
        public void DeleteSession(int id);
        public void AddSession(Session session);
    }
}
