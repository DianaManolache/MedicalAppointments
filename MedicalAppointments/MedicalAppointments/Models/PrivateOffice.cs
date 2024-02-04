using MedicalAppointments.Base;

namespace MedicalAppointments.Models
{
    public class PrivateOffice: BaseEntity
    {
        public Guid DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
}
