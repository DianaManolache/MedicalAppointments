using MedicalAppointments.Data;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using MedicalAppointments.Data.UnitOfWork;
using MedicalAppointments.Repository;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointments.Services.PatientServices
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public IUnitOfWork unitOfWork;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<Patient> CreatePatient(Guid DoctorId, PatientDto patient)
        {
            var patientt = new Patient()
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
            };
            _patientRepository.Add(patient);
            if (await _patientRepository.Save())
            {
                return patientt;
            }
            return null;
        }

        public bool DeletePatient(Patient patient)
        {
            _patientRepository.DeletePatient(patient);
            return _patientRepository.Save();
        }

        public Patient GetPatient(Guid patientId)
        {
            return _patientRepository.GetPatient(patientId);
        }

        public Task<ICollection<Patient>> GetPatients()
        {
            return _patientRepository.GetPatients();
        }

        public bool PatientExists(Guid patientId)
        {
            return _patientRepository.PatientExists(patientId);
        }

        public bool Save()
        {
            return _patientRepository.Save();
        }

        public Task<Patient> UpdatePatient(PatientDto patient)
        {
            _patientRepository.UpdatePatient(patient);
            if (await _patientRepository.Save())
            {
                return patient;
            }
            return null;
        }
    }
}
