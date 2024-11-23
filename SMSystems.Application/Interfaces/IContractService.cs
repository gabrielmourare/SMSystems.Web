using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Interfaces
{
    public interface IContractService
    {

        Task<List<Contract>> GetAll();
        Task<Contract?> GetContractById(int id);
        Task UpdateContract(Contract contract);
        Task DeleteContract(Contract contract);
        Task AddContract(Contract contract);
        Task<bool> ContractExistsAsync(int id);
    }
}
