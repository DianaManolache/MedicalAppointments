using System.ComponentModel.DataAnnotations;

namespace MedicalAppointments.Models.Dto
{
    public class DoctorDto
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid MedicalSpecialityId { get; set; }

    }
}
