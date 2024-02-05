using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;

namespace MedicalAppointments.Services.MedicalSpecialityServices
{
    public interface IMedicalSpecialityService
    {
        Task<ICollection<MedicalSpeciality>> GetMedicalSpecialities();
        Task<ICollection<MedicalSpeciality>> GetDoctorBySpeciality(Guid medicalSpecialityId);
        MedicalSpeciality GetMedicalSpeciality(Guid medicalSpecialityId);
        bool MedicalSpecialityExists(Guid medicalSpecialityId);
        Task<MedicalSpeciality> CreateMedicalSpeciality(MedicalSpecialityDto medicalSpeciality);
        bool Save();
        Task<MedicalSpeciality> UpdateMedicalSpeciality(MedicalSpecialityDto medicalSpeciality);
        Task DeleteMedicalSpeciality(MedicalSpeciality medicalSpeciality);
    }
}
