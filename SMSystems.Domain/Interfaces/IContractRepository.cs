using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface IContractRepository
    {
        Task<List<Contract>> GetAllContracts();
        Task<Contract?> GetContractByIdAsync(int id);
        Task AddContractAsync(Contract contract);
        Task UpdateContractAsync(Contract contract);
        Task DeleteContractAsync(Contract contract);
        Task<bool> ContractExistsAsync(int id);
    }
}
