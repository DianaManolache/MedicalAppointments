﻿using MedicalAppointments.Models;

namespace MedicalAppointments.Interfaces
{
    public interface IPrivateOfficeRepository
    {
        ICollection<PrivateOffice> GetPrivateOffices();
        PrivateOffice GetPrivateOffice(Guid privateOfficeId);
        Task<IEnumerable<PrivateOffice>> GetOfficeByDoctor(Guid doctorId);
        bool PrivateOfficeExists(Guid privateOfficeId);
        bool CreatePrivateOffice(PrivateOffice privateOffice);
        bool Save();
        bool UpdatePrivateOffice(PrivateOffice privateOffice);
        bool DeletePrivateOffice(PrivateOffice privateOffice);

    }
}
