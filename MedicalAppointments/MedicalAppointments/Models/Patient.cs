using MedicalAppointments.Base;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointments.Models
{
    public class Patient: BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<MedicalExamination>? MedicalExamination { get; set; }

    }
}
