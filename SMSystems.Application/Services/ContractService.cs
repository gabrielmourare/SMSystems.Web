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
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contract;
        private readonly IMapper _mapper;

        public ContractService(IMapper mapper, IContractRepository contract)
        {
            _mapper = mapper;
            _contract = contract;
        }

        public async Task AddContract(Contract contract)
        {
            Contract contractmapped = _mapper.Map<Contract>(contract);
            await _contract.AddContractAsync(contractmapped);
        }

        public Task<bool> ContractExistsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContract(Contract patient)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Contract>> GetAll()
        {
            return await _contract.GetAllContracts();
        }

        public async Task<Contract?> GetContractById(int id)
        {
            return await _contract.GetContractByIdAsync(id);
        }

        public Task UpdateContract(Contract patient)
        {
            throw new NotImplementedException();
        }
    }
}
