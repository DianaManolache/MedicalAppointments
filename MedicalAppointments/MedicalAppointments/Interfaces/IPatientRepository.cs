using MedicalAppointments.Models;

namespace MedicalAppointments.Interfaces
{
    public interface IPatientRepository
    {
        ICollection<Patient> GetPatients();
        Patient GetPatient(Guid patientId);
        bool PatientExists(Guid patientId);
        bool CreatePatient(Guid DoctorId,Patient patient);
        bool Save();
    }
}
