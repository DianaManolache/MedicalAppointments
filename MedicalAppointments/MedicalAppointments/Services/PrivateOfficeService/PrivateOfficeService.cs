using MedicalAppointments.Data;
using MedicalAppointments.Data.UnitOfWork;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using MedicalAppointments.Repository;
using Microsoft.EntityFrameworkCore;


namespace MedicalAppointments.Services.PrivateOfficeServices
{
    public class PrivateOfficeService : IPrivateOfficeService
    {
        private readonly IPrivateOfficeRepository _privateOfficeRepository;
        public IUnitOfWork _unitOfWork;

        public PrivateOfficeService(IPrivateOfficeRepository privateOfficeRepository)
        {
            _privateOfficeRepository = privateOfficeRepository;
        }

        public Task<PrivateOffice> CreatePrivateOffice(PrivateOffice privateOffice)
        {
            var po = new PrivateOffice()
            {
                Address = privateOffice.Address,
            };
            _privateOfficeRepository.Add(po);
            if (await _privateOfficeRepository.Save())
            {
                return po;
            }
            return null;
        }

        public Task DeletePrivateOffice(PrivateOffice privateOffice)
        {
            return _privateOfficeRepository.DeletePrivateOffice(privateOffice);
        }

        public PrivateOffice GetOfficeByDoctor(Guid doctorId)
        {
            return _privateOfficeRepository.GetOfficeByDoctor(doctorId);
        }

        public PrivateOffice GetPrivateOffice(Guid privateOfficeId)
        {
            return _privateOfficeRepository.GetPrivateOffice(privateOfficeId);
        }

        public Task<ICollection<PrivateOffice>> GetPrivateOffices()
        {
            return _privateOfficeRepository.GetPrivateOffices();
        }

        public bool PrivateOfficeExists(Guid privateOfficeId)
        {
            return _privateOfficeRepository.PrivateOfficeExists(privateOfficeId);
        }

        public bool Save()
        {
            return _privateOfficeRepository.Save();
        }

        public Task<PrivateOffice> UpdatePrivateOffice(PrivateOffice privateOffice)
        {
            var po = new PrivateOffice()
            {
                Address = privateOffice.Address,
            };
            _privateOfficeRepository.UpdatePrivateOffice(po);
            if (await _privateOfficeRepository.Save())
            {
                return po;
            }
            return null;
        }
    }
}
