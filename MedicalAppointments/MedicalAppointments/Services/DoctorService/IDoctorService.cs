using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;

namespace MedicalAppointments.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<ICollection<Doctor>> GetDoctors();
        Doctor GetDoctor(Guid doctorId);
        Doctor GetDoctor(string FirstName, string LastName);
        bool DoctorExists(Guid doctorId);
        Task<Doctor> CreateDoctor(Guid PatientId, DoctorDto doctor);
        bool Save();
        Task<Doctor> UpdateDoctor(Guid PatientId, DoctorDto doctor);
        Task DeleteDoctor(Doctor doctor);

    }
}
