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
        IQueryable<Invoice> GetAllInvoices();
        Task<Invoice?> GetInvoiceByIdAsync(int id);
        IQueryable<Invoice> GetInvoicesByPatientId(int patientId);
        Task AddInvoiceAsync(Invoice invoice);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task DeleteInvoiceAsync(Invoice invoice);
        IQueryable<Session> GetAllSessions(int patientId);
    }

}
