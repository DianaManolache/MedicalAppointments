namespace MedicalAppointments.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public MedicalSpeciality MedicalSpeciality { get; set; }
        public PrivateOffice PrivateOffice { get; set; }
        public ICollection<MedicalExamination> MedicalExaminations { get; set; }

    }
}
