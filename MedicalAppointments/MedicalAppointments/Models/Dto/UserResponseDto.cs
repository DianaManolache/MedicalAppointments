using System.ComponentModel.DataAnnotations;

namespace MedicalAppointments.Models.Dto
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
