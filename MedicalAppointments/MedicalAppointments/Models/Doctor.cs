namespace MedicalAppointments.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MedicalSpeciality MedicalSpeciality { get; set; }
        public PrivateOffice? Office { get; set; }
        public ICollection<MedicalExamination> MedicalExaminations { get; set; }

    }
}
