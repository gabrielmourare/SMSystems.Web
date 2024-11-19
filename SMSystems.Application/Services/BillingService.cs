using AutoMapper;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Services
{
    public class BillingService : IBillingService
    {
        private IBillingRepository _billing;
        private readonly IMapper _mapper;

        public BillingService(IMapper mapper, IBillingRepository billing)
        {
            _mapper = mapper;
            _billing = billing;
        }
        public async Task AddBilling(Billing billing)
        {
            Billing billingMaped = _mapper.Map<Billing>(billing);
            await _billing.AddBillingAsync(billingMaped);
        }

        public async Task<bool> BillingExistsAsync(int id)
        {
            return await _billing.BillingExistsAsync(id);

        }

        public async Task DeleteBilling(Billing billing)
        {
            if (billing != null)
            {
                await _billing.DeleteBillingAsync(billing);
            }
        }

        public Task<List<Billing>> GetAll()
        {
            return _billing.GetAllBillings();
        }

        public async Task<Billing?> GetBillingById(int id)
        {
            return await _billing.GetBillingByIdAsync(id);
        }

        public async Task UpdateBilling(Billing billing)
        {
            await _billing.UpdateBillingAsync(billing); ;
        }
    }
}
