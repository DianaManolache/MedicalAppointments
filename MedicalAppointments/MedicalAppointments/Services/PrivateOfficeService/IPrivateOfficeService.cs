using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;

namespace MedicalAppointments.Services.PrivateOfficeServices
{
    public interface IPrivateOfficeService
    {
        Task<ICollection<PrivateOffice>> GetPrivateOffices();
        PrivateOffice GetPrivateOffice(Guid privateOfficeId);
        PrivateOffice GetOfficeByDoctor(Guid doctorId);
        bool PrivateOfficeExists(Guid privateOfficeId);
        Task<PrivateOffice> CreatePrivateOffice(PrivateOffice privateOffice);
        bool Save();
        Task<PrivateOffice> UpdatePrivateOffice(PrivateOffice privateOffice);
        Task DeletePrivateOffice(PrivateOffice privateOffice);

    }
}
