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
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoice;
        private readonly ISessionRepository _session;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoice, ISessionRepository session, IMapper mapper)
        {
            _invoice = invoice;
            _session = session;
            _mapper = mapper;
        }

        public IQueryable<Invoice> GetAll()
        {
            return _invoice.GetAllInvoices();
        }

        public IQueryable<Invoice> GetAllPatientInvoices(int patientId)
        {
            return _invoice.GetInvoicesByPatientId(patientId);
        }

        public IQueryable<Session> GetAllInvoiceSessions(int patientId)
        {
            return _invoice.GetAllSessions(patientId);
        }

        public async Task<Invoice> GetInvoiceByIdAsync(int invoiceId)
        {
            var invoice = await _invoice.GetInvoiceByIdAsync(invoiceId);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }
            return invoice;
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            Invoice invoiceMapped = _mapper.Map<Invoice>(invoice);

            await _invoice.AddInvoiceAsync(invoiceMapped);


        }

        public async Task UpdateInvoiceAsync(int id)
        {
            var invoice = await _invoice.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }
            await _invoice.UpdateInvoiceAsync(invoice);
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _invoice.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }              

            await _invoice.DeleteInvoiceAsync(invoice);
        }


    }

}
