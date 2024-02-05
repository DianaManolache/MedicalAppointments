using MedicalAppointments.Interfaces;
using MedicalAppointments.Repository;



namespace MedicalAppointments.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDoctorRepository DoctorRepository { get; set; }
        public IPrivateOfficeRepository PrivateOfficeRepository { get; set; }
        public IPatientRepository PatientRepository { get; set; }
        public IMedicalSpecialityRepository MedicalSpecialityRepository { get; set; }
        
        private MedicalAppointmentsDbContext _context;
        public UnitOfWork(MedicalAppointmentsDbContext context)
        {
            DoctorRepository = new DoctorRepository(_context);
            PrivateOfficeRepository = new PrivateOfficeRepository(_context);
            PatientRepository = new PatientRepository(_context);
            MedicalSpecialityRepository = new MedicalSpecialityRepository(_context);
            _context = context;
        }
        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
