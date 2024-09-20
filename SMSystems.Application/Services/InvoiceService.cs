using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.DTOs;
using SMSystems.Application.Interfaces;
using SMSystems.Data.Repositories;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;
using SMSystems.Printer.Services;
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
        private readonly IPatientRepository _patient;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoice, ISessionRepository session, IMapper mapper, IPatientRepository patient)
        {
            _invoice = invoice;
            _session = session;
            _mapper = mapper;
            _patient = patient;
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

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }


            Invoice invoiceMapped = _mapper.Map<Invoice>(invoice);

            await _invoice.UpdateInvoiceAsync(invoiceMapped);
        }

        public async Task DeleteInvoiceAsync(int id)
        {
            var invoice = await _invoice.GetInvoiceByIdAsync(id);
            if (invoice == null)
            {
                throw new KeyNotFoundException("Invoice not found");
            }

            Invoice invoiceMapped = _mapper.Map<Invoice>(invoice);

            await _invoice.DeleteInvoiceAsync(invoiceMapped);
        }

        public async Task<bool> InvoiceExistsAsync(int id)
        {
            return await _invoice.InvoiceExistsAsync(id);
        }

        public async Task<InvoiceDetailsDTO> GetInvoiceDetails(int invoiceId)
        {
            Invoice? invoice = await _invoice.GetInvoiceByIdAsync(invoiceId);
            Patient? patient = await _patient.GetPatientByIdAsync(invoice.PatientID);
            List<Session> sessions = invoice.Sessions.ToList();

            return new InvoiceDetailsDTO()
            {
                PatientName = patient.Name,
                City = "São Caetano do Sul",
                ProfessionalRCNumber = "06/87574",
                PatientSocialNumber = patient.SocialNumber,
                Profession = "Psicóloga",
                ProfessionalName = "Andiara Sarraf",
                ProfessionalSocialNumber = "066.171.616-37",
                EmissionDate = DateTime.Now.ToString("dd/MM/yyyy"),
                FullAddress = " ",
                SessionValue = invoice.SessionValue.ToString("C"),
                Telephone = "+55 (11) 984614824",
                TotalValue = invoice.TotalValue,
                WrittenTotal = ConverterExtenso.toExtenso(invoice.TotalValue),
                PatientBirthDate = patient.BirthDate

            };

        }
        public async Task<List<Invoice>> GetInvoicesByIdsAsync(List<int> ids)
        {
            return await _invoice.GetInvoicesByIdsAsync(ids);
        }

        public async Task DeleteInvoicesAsync(List<int> ids)
        {
            await _invoice.DeleteInvoicesAsync(ids);
        }

    }

}
