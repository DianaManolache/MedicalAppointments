using MedicalAppointments.Models;

namespace MedicalAppointments.Interfaces
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(Guid doctorId);
        Doctor GetDoctor(string FirstName, string LastName);
        bool DoctorExists(Guid doctorId);
        bool CreateDoctor(Guid PatientId, Doctor doctor);
        bool Save();

    }
}
