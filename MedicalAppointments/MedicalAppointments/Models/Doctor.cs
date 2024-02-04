using MedicalAppointments.Base;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointments.Models
{
    public class Doctor: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid? MedicalSpecialityId { get; set; }
        public MedicalSpeciality? MedicalSpeciality { get; set; }
        public PrivateOffice? PrivateOffice { get; set; }
        public ICollection<MedicalExamination>? MedicalExamination { get; set; }
    }
}
