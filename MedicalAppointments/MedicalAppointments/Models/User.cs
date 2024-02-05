using MedicalAppointments.Base;
using MedicalAppointments.Models.Enum;
using System.Text.Json.Serialization;

namespace MedicalAppointments.Models
{
    public class User //cu refresh token
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime TokenCreated { get; set; }
        public DateTime TokenExpires { get; set; }
    }
}
