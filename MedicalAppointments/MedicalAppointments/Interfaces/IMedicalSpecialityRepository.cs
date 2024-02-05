using MedicalAppointments.Models;

namespace MedicalAppointments.Interfaces
{
    public interface IMedicalSpecialityRepository
    {
        ICollection<MedicalSpeciality> GetMedicalSpecialities();
        ICollection<MedicalSpeciality> GetDoctorBySpeciality(Guid medicalSpecialityId);
        MedicalSpeciality GetMedicalSpeciality(Guid medicalSpecialityId);
        bool MedicalSpecialityExists(Guid medicalSpecialityId);
        bool CreateMedicalSpeciality(MedicalSpeciality medicalSpeciality);
        bool Save();
    }
}
