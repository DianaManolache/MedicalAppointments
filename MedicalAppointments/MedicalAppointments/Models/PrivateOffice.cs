namespace MedicalAppointments.Models
{
    public class PrivateOffice
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;

    }
}
