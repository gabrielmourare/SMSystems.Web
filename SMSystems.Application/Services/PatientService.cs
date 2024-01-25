using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SMSystems.Application.Interfaces;
using SMSystems.Domain.Entities;
using SMSystems.Domain.Interfaces;

namespace SMSystems.Application.Services;

public class PatientService : IPatientService
{
    private readonly IInvoiceRepository _invoice;
    private readonly IPatientRepository _patient;
    private readonly ISessionRepository _session;

    public PatientService(IPatientRepository patient, IInvoiceRepository invoice, ISessionRepository session)
    {
        _patient = patient;
        _session = session;
        _invoice = invoice;
    }

    public IQueryable<Invoice> ObtainAllInvoices(int patientId)
    {
        return _invoice.GetInvoicesByPatientId(patientId);
    }

    public IQueryable<Session> GetAllPatientSessions(int patientId)
    {
        return _session.GetAllPatientSessions(patientId);
    }

    public Patient GetPatientData(int patientId)
    {
        return _patient.GetPatientById(patientId);
    }

    public IQueryable<Patient> GetAll()
    {
        return _patient.GetAllPatients();
    }

    public Patient GetPatientById(int id)
    {
        return _patient.GetPatientById(id);
    }

    public void UpdatePatient(Patient patient)
    {
        _patient.UpdatePatient(patient.ID);
    }

    public void DeletePatient(int id)
    {
        _patient.DeletePatient(id);
    }

    public void AddPatient(Patient patient)
    {
        _patient.SavePatient(patient);
    }

    public Patient GetPatientBySN(string socialNumber)
    {
        return _patient.GetPatientBySocialNumber(socialNumber);
    }
   
}

