using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Interfaces
{
    public interface IBillingService
    {
        Task<List<Billing>> GetAll();
        Task<Billing?> GetBillingById(int id);
        Task UpdateBilling(Billing billing);
        Task DeleteBilling(Billing billing);
        Task AddBilling(Billing billing);
        Task<bool> BillingExistsAsync(int id);
    }
}
