using MedicalAppointments.Models;

namespace MedicalAppointments.Specifications
{
    public class DoctorSpecification : Specification<Doctor>
    {
        public DoctorSpecification() : base(p => p.PrivateOffice != null)
        {
            AddInclude(d => d.MedicalExamination);
        }
    }
}
