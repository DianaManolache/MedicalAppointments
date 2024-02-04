using MedicalAppointments.Models;

namespace MedicalAppointments.Interfaces
{
    public interface IPrivateOfficeRepository
    {
        ICollection<PrivateOffice> GetPrivateOffices();
        PrivateOffice GetPrivateOffice(Guid privateOfficeId);
        PrivateOffice GetOfficeByDoctor(Guid doctorId);
        bool PrivateOfficeExists(Guid privateOfficeId);

    }
}
