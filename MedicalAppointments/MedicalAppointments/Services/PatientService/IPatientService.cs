using MedicalAppointments.Models;

namespace MedicalAppointments.Services.PatientServices
{
    public interface IPatientService
    {
        Task<ICollection<Patient>> GetPatients();
        Patient GetPatient(Guid patientId);
        bool PatientExists(Guid patientId);
        Task <Patient> CreatePatient(Guid DoctorId, PatientDto patient);
        bool Save();
        Task <Patient> UpdatePatient(PatientDto patient);
        bool DeletePatient(Patient patient);
    }
}
