using MedicalAppointments.Base;
using System.ComponentModel.DataAnnotations;

namespace MedicalAppointments.Models
{
    public class MedicalSpeciality: BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Doctor>? Doctors { get; set; }

    }
}
