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

        IQueryable<Contract> GetAll();
        Task<Contract?> GetContractById(int id);
        Task UpdateContract(Contract patient);
        Task DeleteContract(Contract patient);
        Task AddContract(Contract patientViewModel);
        Task<bool> ContractExistsAsync(int id);
    }
}
