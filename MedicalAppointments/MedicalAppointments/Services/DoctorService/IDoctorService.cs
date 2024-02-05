using MedicalAppointments.Models;

namespace MedicalAppointments.Services.DoctorServices
{
    public interface IDoctorRepository
    {
        ICollection<Doctor> GetDoctors();
        Doctor GetDoctor(Guid doctorId);
        Doctor GetDoctor(string FirstName, string LastName);
        bool DoctorExists(Guid doctorId);
        bool CreateDoctor(Guid PatientId, Doctor doctor);
        bool Save();
        bool UpdateDoctor(Guid PatientId, Doctor doctor);
        bool DeleteDoctor(Doctor doctor);

    }
}
