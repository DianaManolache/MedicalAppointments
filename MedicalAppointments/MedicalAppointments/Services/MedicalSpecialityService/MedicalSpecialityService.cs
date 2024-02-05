using MedicalAppointments.Data;
using MedicalAppointments.Models;
using MedicalAppointments.Repository;
using MedicalAppointments.Dto;
using MedicalAppointments.UnitOfWork;
using MedicalAppointments.Models.Dto;

namespace MedicalAppointments.Services.MedicalSpecialityServices
{
    public class MedicalSpecialityService : IMedicalSpecialityService
    {
        private readonly IMedicalSpecialityRepository _medicalSpecialityRepository;
        public IUnitOfWork _unitOfWork;
        public MedicalSpecialityService(IMedicalSpecialityRepository medicalSpecialityRepository)
        {
            _medicalSpecialityRepository = medicalSpecialityRepository;
        }

        public async Task<MedicalSpeciality> CreateMedicalSpeciality(MedicalSpecialityDto medicalSpeciality)
        {
            var ms = new MedicalSpeciality()
            {
                Name = medicalSpeciality.Name,
                Description = medicalSpeciality.Description
            };
            _medicalSpecialityRepository.Add(ms);
            if(await _medicalSpecialityRepository.Save())
            {
                return ms;
            }
            return null;
        }

        public async Task DeleteMedicalSpeciality(MedicalSpeciality medicalSpeciality)
        {
            _medicalSpecialityRepository.DeleteMedicalSpeciality(medicalSpeciality);
            await _medicalSpecialityRepository.Save();
        }

        public Task<ICollection<MedicalSpeciality>> GetDoctorBySpeciality(Guid medicalSpecialityId)
        {
            return _medicalSpecialityRepository.GetDoctorBySpeciality(medicalSpecialityId);
        }

        public Task<ICollection<MedicalSpeciality>> GetMedicalSpecialities()
        {
            return _medicalSpecialityRepository.GetMedicalSpecialities();
        }

        public MedicalSpeciality GetMedicalSpeciality(Guid medicalSpecialityId)
        {
            return _medicalSpecialityRepository.GetMedicalSpeciality(medicalSpecialityId);
        }

        public bool MedicalSpecialityExists(Guid medicalSpecialityId)
        {
            return _medicalSpecialityRepository.MedicalSpecialityExists(medicalSpecialityId);
        }

        public bool Save()
        {
            return _medicalSpecialityRepository.Save();
        }

        public Task<MedicalSpeciality> UpdateMedicalSpeciality(MedicalSpecialityDto medicalSpeciality)
        {
            var ms = _medicalSpecialityRepository.GetMedicalSpeciality(medicalSpeciality.Id);
            if (ms == null)
            {
                return null;
            }
            ms.Name = medicalSpeciality.Name;

            _medicalSpecialityRepository.UpdateMedicalSpeciality(ms);
            if (await _medicalSpecialityRepository.Save())
            {
                return ms;
            }
            else
            {
                return null;
            }
        }
    }
}
