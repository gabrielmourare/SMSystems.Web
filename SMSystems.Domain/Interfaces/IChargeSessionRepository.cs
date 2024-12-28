using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSystems.Domain.Entities;

namespace SMSystems.Domain.Interfaces
{
    public interface IChargeSessionRepository
    {
        Task<List<ChargeSession?>> GetAllChargeSessions();
        Task<List<ChargeSession?>> GetAllPatientChargeSessions(int patientId);
        Task<ChargeSession?> GetChargeSessionByIdAsync(int id);
        Task SaveChargeSessionAsync(ChargeSession chargeSession);
        Task UpdateChargeSessionAsync(ChargeSession chargeSession);
        Task DeleteChargeSessionAsync(List<ChargeSession> chargeSessions);
        Task<List<ChargeSession>> GetAllChargeSessions(int chargeId);
    }
}
