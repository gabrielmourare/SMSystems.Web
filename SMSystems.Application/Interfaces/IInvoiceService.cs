using SMSystems.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMSystems.Application.Interfaces
{
    public interface IInvoiceService
    {
        IQueryable<Invoice> GetAll();
        Task<Invoice?> GetInvoiceByIdAsync(int invoiceId);
        IQueryable<Invoice> GetAllPatientInvoices(int patientId);
        IQueryable<Session> GetAllInvoiceSessions(int patientId);
        Task DeleteInvoiceAsync(int invoiceId);
        Task UpdateInvoiceAsync(Invoice invoice);
        Task AddInvoiceAsync(Invoice invoice);
        Task<bool> InvoiceExistsAsync(int id);
    }

}
