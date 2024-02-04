namespace MedicalAppointments.Models
{
    public class MedicalExamination
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set;}

    }
}
