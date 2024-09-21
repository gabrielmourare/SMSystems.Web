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
    public class ContractRepository : IContractRepository, IDisposable
    {
        private readonly SMSystemsDBContext _context;
        private bool _disposed = false;

        public ContractRepository(SMSystemsDBContext context)
        {
            _context = context;
        }
        public async Task AddContractAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
            await SaveAsync();
        }

        public async Task DeleteContractAsync(Contract contract)
        {
            if (contract != null)
            {
                _context.Contracts.Remove(contract);
                await SaveAsync();
            }
        }

        public async Task<List<Contract>> GetAllContracts()
        {
            return await _context.Contracts.ToListAsync();
        }

        public async Task<Contract?> GetContractByIdAsync(int id)
        {
            return await _context.Contracts.FindAsync(id);
        }

        public async Task UpdateContractAsync(Contract contract)
        {
            _context.Contracts.Update(contract);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ContractExistsAsync(int id)
        {
            return await _context.Contracts.AnyAsync(e => e.ID == id);
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

    }
}
