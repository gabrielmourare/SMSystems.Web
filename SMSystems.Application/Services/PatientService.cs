﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;

namespace SMSystems.Application.Services;

public class PatientService : IPatientService
{
    private readonly IInvoiceRepository _invoice;
    private readonly IPatientRepository _patient;
    private readonly ISessionRepository _session;
    private readonly IMapper _mapper;

    public PatientService(IPatientRepository patient, IInvoiceRepository invoice, ISessionRepository session, IMapper mapper)
    {
        _patient = patient;
        _session = session;
        _invoice = invoice;
        _mapper = mapper;
    }

    public IQueryable<Invoice> ObtainAllInvoices(int patientId)
    {
        return _invoice.GetInvoicesByPatientId(patientId);
    }

    public IQueryable<Session> GetAllPatientSessions(int patientId)
    {
        return _session.GetAllPatientSessions(patientId);
    }

    public async Task<Patient> GetPatientData(int patientId)
    {
        return await _patient.GetPatientByIdAsync(patientId);
    }

    public IQueryable<Patient> GetAll()
    {
        return _patient.GetAllPatients();
    }

    public async Task<Patient> GetPatientById(int id)
    {
        return await _patient.GetPatientByIdAsync(id);
    }

    public async Task UpdatePatient(Patient patient)
    {
        await _patient.UpdatePatientAsync(patient);
    }

    public async Task DeletePatient(int id)
    {
        await _patient.DeletePatientAsync(id);
    }

    public async Task AddPatient(Patient patientViewModel)
    {
        Patient patient = _mapper.Map<Patient>(patientViewModel);
        await _patient.SavePatientAsync(patient);
    }

    public async Task<Patient> GetPatientBySN(string socialNumber)
    {
        return await _patient.GetPatientBySocialNumberAsync(socialNumber);
    }

    public async Task<bool> PatientExistsAsync(int id)
    {
        return await _patient.PatientExistsAsync(id);
    }

}

