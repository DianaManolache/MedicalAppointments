using MedicalAppointments.Data;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;

namespace MedicalAppointments.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly DataContext _context;

        public PatientRepository(DataContext context)
        {
            _context = context;
        }

        public Patient GetPatient(Guid patientId)
        {
            return _context.Patients.Where(p => p.Id == patientId).FirstOrDefault();
        }

        public ICollection<Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }

        public bool PatientExists(Guid patientId)
        {
            return _context.Patients.Any(p => p.Id == patientId);
        }
        public bool CreatePatient(Guid DoctorId, Patient patient)
        {
            var examinationEntity = _context.Doctors.Where(d=>d.Id == DoctorId).FirstOrDefault();
            var examination = new MedicalExamination
            {
                Doctor = examinationEntity,
                Patient = patient
            };
            _context.Add(examination);
            _context.Add(patient);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool UpdatePatient(Patient patient)
        {
            var updated = _context.SaveChanges();
            return updated >= 0 ? true : false;
        }

        public bool DeletePatient(Patient patient)
        {
            _context.Patients.Remove(patient);
            return Save();
        }
    }
}
