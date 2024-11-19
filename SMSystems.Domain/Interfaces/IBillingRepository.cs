using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface IBillingRepository
    {
        Task<List<Billing>> GetAllBillings();
        Task<Billing?> GetBillingByIdAsync(int id);
        Task AddBillingAsync(Billing billing);
        Task UpdateBillingAsync(Billing billing);
        Task DeleteBillingAsync(Billing billing);
        Task<bool> BillingExistsAsync(int id);
    }
}
