using MedicalAppointments.Models;

namespace MedicalAppointments.Services.PrivateOfficeServices
{
    public interface IPrivateOfficeRepository
    {
        ICollection<PrivateOffice> GetPrivateOffices();
        PrivateOffice GetPrivateOffice(Guid privateOfficeId);
        PrivateOffice GetOfficeByDoctor(Guid doctorId);
        bool PrivateOfficeExists(Guid privateOfficeId);
        bool CreatePrivateOffice(PrivateOffice privateOffice);
        bool Save();
        bool UpdatePrivateOffice(PrivateOffice privateOffice);
        bool DeletePrivateOffice(PrivateOffice privateOffice);

    }
}
