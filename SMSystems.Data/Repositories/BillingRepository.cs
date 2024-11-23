using Microsoft.EntityFrameworkCore;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Data.Repositories
{
    public class BillingRepository : IBillingRepository, IDisposable
    {
        private readonly SMSystemsDBContext _context;
        private bool _disposed = false;

        public BillingRepository(SMSystemsDBContext context)
        {
            _context = context;
        }

        public async Task AddBillingAsync(Billing billing)
        {
            await _context.Billings.AddAsync(billing);
            await SaveAsync();

        }

        public async Task DeleteBillingAsync(Billing billing)
        {
            if (billing != null)
            {
                _context.Billings.Remove(billing);
                await SaveAsync();
            }
        }

        public async Task<List<Billing>> GetAllBillings()
        {
            return await _context.Billings.ToListAsync();
        }


        public async Task<Billing?> GetBillingByIdAsync(int id)
        {
            return await _context.Billings.FindAsync(id);
        }

        public async Task UpdateBillingAsync(Billing billing)
        {
            _context.Billings.Update(billing);
            await SaveAsync();
        }

        public async Task<bool> BillingExistsAsync(int id)
        {
            return await _context.Billings.AnyAsync(e => e.ID == id);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
