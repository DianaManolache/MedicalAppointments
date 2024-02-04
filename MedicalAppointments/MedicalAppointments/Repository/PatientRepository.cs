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
    }
}
