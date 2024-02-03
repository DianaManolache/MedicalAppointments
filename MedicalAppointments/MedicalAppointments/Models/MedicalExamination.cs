namespace MedicalAppointments.Models
{
    public class MedicalExamination
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set;}

    }
}
