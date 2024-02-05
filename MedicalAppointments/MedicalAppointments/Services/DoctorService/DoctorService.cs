using MedicalAppointments.Data;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using MedicalAppointments.Repository;
using Microsoft.EntityFrameworkCore;
using MedicalAppointments.Data.UnitOfWork;



namespace MedicalAppointments.Services.DoctorService
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public IUnitOfWork unitOfWork;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public Task<Doctor> CreateDoctor(Guid PatientId, DoctorDto doctor)
        {
            var examinationEntity = _doctorRepository.Patients.Where(a => a.Id == pacientId).FirstOrDefault();
            var examination = new MedicalExamination()
            {
                Patient = examinationEntity,
                Doctor = doctor,
            };
            _doctorRepository.Add(examination);

            _doctorRepository.Add(doctor);
            return Save();
        }

        public async Task DeleteDoctor(Doctor doctor)
        {
            _doctorRepository.DeleteDoctor(doctor);
            await _doctorRepository.Save();
        }

        public bool DoctorExists(Guid doctorId)
        {
            var doctor = _doctorRepository.GetDoctor(doctorId);
            return doctor != null;
        }

        public Doctor GetDoctor(Guid doctorId)
        {
            return _doctorRepository.GetDoctor(doctorId);
        }

        public Doctor GetDoctor(string FirstName, string LastName)
        {
            return _doctorRepository.GetDoctor(FirstName, LastName);
        }

        public async Task<ICollection<Doctor>> GetDoctors()
        {
            return await _doctorRepository.GetDoctors();
        }

        public bool Save()
        {
            return _doctorRepository.Save();
        }

        public Task<Doctor> UpdateDoctor(Guid PatientId, DoctorDto doctor)
        {
            var doc = _doctorRepository.GetDoctor(doctor.Id);
            if (doc == null)
            {
                return null;
            }
            doc.FirstName = doctor.FirstName;
            doc.LastName = doctor.LastName;

            _doctorRepository.UpdateDoctor(doc);
            if(await _doctorRepository.Save())
            {
                return doc;
            }
            else
            {
                return null;
            }
        }
    }
}
