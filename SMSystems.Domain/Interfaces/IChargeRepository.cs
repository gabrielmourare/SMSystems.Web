using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSystems.Domain.Entities;

namespace SMSystems.Domain.Interfaces
{
    public interface IChargeRepository
    {
        Task<List<Charge>> GetAllCharges();
        Task<Charge?> GetChargeByIdAsync(int id);
        Task<List<Charge>> GetChargesByPatientId(int patientId);
        Task AddChargeAsync(Charge charge);
        Task UpdateChargeAsync(Charge charge);
        Task DeleteChargeAsync(Charge charge);
        Task<List<ChargeSession>> GetAllChargeSessions(int patientId);
        Task<bool> ChargeExistsAsync(int id);
        Task<List<Charge>> GetChargesByIdsAsync(List<int> ids);
        Task DeleteChargesAsync(List<int> ids);

    }
}
