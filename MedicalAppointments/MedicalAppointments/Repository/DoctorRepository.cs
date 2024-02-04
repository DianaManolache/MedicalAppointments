using MedicalAppointments.Data;
using MedicalAppointments.Interfaces;
using MedicalAppointments.Models;

namespace MedicalAppointments.Repository
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
    }
}
