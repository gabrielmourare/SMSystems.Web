using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;

namespace SMSystems.Data.Repositories
{
    public class ChargeSessionRepository : IChargeSessionRepository
    {
        private readonly SMSystemsDBContext _context;

        public ChargeSessionRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public async Task DeleteChargeSessionAsync(List<ChargeSession> chargeSessions)
        {
            foreach (ChargeSession chargeSession in chargeSessions)
            {
                _context.ChargeSessions.Remove(chargeSession);

            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<ChargeSession>> GetAllChargeSessions()
        {
            return await _context.ChargeSessions.ToListAsync();
        }

        public Task<List<ChargeSession>> GetAllChargeSessions(int chargeId)
        {
            return _context.ChargeSessions.Where(chargeSession => chargeSession.ChargeID == chargeId).ToListAsync();
        }

        public async Task<List<ChargeSession>> GetAllPatientChargeSessions(int patientId)
        {
            return await _context.ChargeSessions.Where(session => session.PatientID == patientId).ToListAsync();
        }

        public async Task<ChargeSession?> GetChargeSessionByIdAsync(int id)
        {
            return await _context.ChargeSessions.FindAsync(id);
        }

        public async Task SaveChargeSessionAsync(ChargeSession chargeSession)
        {
            await _context.ChargeSessions.AddAsync(chargeSession);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChargeSessionAsync(ChargeSession chargeSession)
        {
            _context.ChargeSessions.Update(chargeSession);
            await _context.SaveChangesAsync();
        }
    }
}
