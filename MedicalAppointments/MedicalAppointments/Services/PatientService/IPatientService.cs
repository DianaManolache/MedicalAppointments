using MedicalAppointments.Models;

namespace MedicalAppointments.Services.PatientServices
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(Guid patientId);
        bool PatientExists(Guid patientId);
        bool CreatePatient(Guid DoctorId, Patient patient);
        bool Save();
        bool UpdatePatient(Patient patient);
        bool DeletePatient(Patient patient);
    }
}
