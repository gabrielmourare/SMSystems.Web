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
    public class ChargeRepository : IChargeRepository
    {
        private readonly SMSystemsDBContext _context;

        public ChargeRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public async Task AddChargeAsync(Charge charge)
        {
            await _context.Charges.AddAsync(charge);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ChargeExistsAsync(int id)
        {
            return await _context.Charges.AnyAsync(e => e.ID == id);
        }

        public async Task DeleteChargeAsync(Charge charge)
        {
            _context.Remove(charge);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChargesAsync(List<int> ids)
        {
            var charges = await _context.Charges.Where(c => ids.Contains(c.ID)).ToListAsync();
            _context.Charges.RemoveRange(charges);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Charge>> GetAllCharges()
        {
            return await _context.Charges.ToListAsync();
        }

        public async Task<List<ChargeSession>> GetAllChargeSessions(int patientId)
        {
            return await _context.ChargeSessions.Where(chargeSession => chargeSession.PatientID == patientId).ToListAsync();
        }

        public async Task<Charge> GetChargeByIdAsync(int id)
        {
            Charge? charge = await _context.Charges
                                           .Include(c => c.ChargeSessions)
                                           .FirstOrDefaultAsync(c => c.ID == id);

            return charge;
        }

        public async Task<List<Charge>> GetChargesByIdsAsync(List<int> ids)
        {
            return await _context.Charges.Where(i => ids.Contains(i.ID)).ToListAsync();
        }

        public async Task<List<Charge>> GetChargesByPatientId(int patientId)
        {
            return await _context.Charges.Include(charge => charge.ChargeSessions).Where(charge => charge.PatientID == patientId).ToListAsync();
        }

        public async Task UpdateChargeAsync(Charge charge)
        {
            var existingCharge = await _context.Charges
                                               .Include(c => c.ChargeSessions)
                                               .FirstOrDefaultAsync(c => c.ID == charge.ID);

            if (existingCharge == null)
            {
                throw new Exception("Charge not found");
            }

            _context.Entry(existingCharge).CurrentValues.SetValues(charge);

            UpdateChargeSessions(existingCharge, charge);

            await _context.SaveChangesAsync();
        }

        private void UpdateChargeSessions(Charge existingCharge, Charge updatedCharge)
        {
            // Mapeia as sessions existentes pelo ID
            var existingSessionsMap = existingCharge.ChargeSessions.ToDictionary(s => s.ID);

            // Remove as sessions que não estão na lista atualizada
            foreach (var existingSession in existingCharge.ChargeSessions.ToList())
            {
                if (!updatedCharge.ChargeSessions.Any(s => s.ID == existingSession.ID))
                {
                    _context.ChargeSessions.Remove(existingSession);
                }
            }

            // Atualiza ou adiciona as sessions
            foreach (var updatedSession in updatedCharge.ChargeSessions)
            {
                if (existingSessionsMap.TryGetValue(updatedSession.ID, out var existingSession))
                {
                    // Atualiza a session existente
                    _context.Entry(existingSession).CurrentValues.SetValues(updatedSession);
                }
                else
                {
                    // Adiciona uma nova session
                    existingCharge.ChargeSessions.Add(updatedSession);
                }
            }
        }
    }
}
