using MedicalAppointments.Base;
using MedicalAppointments.Models.Enum;
using System.Text.Json.Serialization;

namespace MedicalAppointments.Models
{
    public class User: BaseEntity
    {
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
