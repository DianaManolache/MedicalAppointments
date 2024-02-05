using MedicalAppointments.Data;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;

namespace MedicalAppointments.Services.DoctorService
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly DataContext _context;
        public DoctorRepository(DataContext context)
        {
            _context = context;
        }

        public Doctor GetDoctor(Guid doctorId)
        {
            return _context.Doctors.Where(d => d.Id == doctorId).FirstOrDefault();
        }

        public Doctor GetDoctor(string FirstName, string LastName)
        {
            return _context.Doctors.Where(d => d.FirstName == FirstName && d.LastName == LastName).FirstOrDefault();
        }

        public ICollection<Doctor> GetDoctors()
        {
            return _context.Doctors.OrderBy(d => d.Id).ToList();
        }
        public bool DoctorExists(Guid doctorId)
        {
            return _context.Doctors.Any(d => d.Id == doctorId);
        }

        public bool CreateDoctor(Guid pacientId, Doctor doctor)
        {
            var examinationEntity = _context.Patients.Where(a => a.Id == pacientId).FirstOrDefault();
            var examination = new MedicalExamination()
            {
                Patient = examinationEntity,
                Doctor = doctor,
            };
            _context.Add(examination);

            _context.Add(doctor);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdateDoctor(Guid PatientId, Doctor doctor)
        {
            _context.Update(doctor);
            return Save();
        }

        public bool DeleteDoctor(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            return Save();
        }
    }
}
