namespace MedicalAppointments.Models
{
    public class MedicalSpeciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }

    }
}
