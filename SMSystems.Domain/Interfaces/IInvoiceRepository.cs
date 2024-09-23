using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Domain.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<List<Invoice>> GetAllInvoices();
        Task<Invoice?> GetInvoiceByIdAsync(int id);
        Task<List<Invoice>> GetInvoicesByPatientId(int patientId);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(Invoice invoice);
        Task<List<Session>> GetAllSessions(int patientId);
        Task<bool> InvoiceExistsAsync(int id);
        Task<List<Invoice>> GetInvoicesByIdsAsync(List<int> ids);
        Task DeleteInvoicesAsync(List<int> ids);

    }

}
