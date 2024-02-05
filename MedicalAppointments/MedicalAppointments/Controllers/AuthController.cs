using MedicalAppointments.Models;
using MedicalAppointments.Models.Dto;
using MedicalAppointments.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MedicalAppointments.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = userService;
        }
        [HttpGet, Authorize]
        public ActionResult<string> GetMyName()
        {
            return Ok(_userService.GetMyName());

           /* var userName = User?.Identity?.Name;
            var roleClaims = User?.FindAll(ClaimTypes.Role);
            var roles = roleClaims?.Select(c => c.Value).ToList();
            var roles2 = User?.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();
            return Ok(new { userName, roles, roles2 });*/
        }

        [HttpPost("register")]
        public ActionResult<User> Register(UserDto request)
        {
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(request.Password);

            user.Username = request.Username;
            user.PasswordHash = passwordHash;

            return Ok(user);
        }

        [HttpPost("login")]
        public ActionResult<User> Login(UserDto request)
        {
            if (user.Username != request.Username)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return BadRequest("Wrong password.");
            }
            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);
            return Ok(user);
        }
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if(!user.RefreshToken.Equals(refreshToken))
            {
                return Unauthorized("Invalid Refresh Token");
            }
            else if(user.TokenExpires <DateTime.Now)
            {
                return Unauthorized("Token expired.");
            }
            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }
        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7)
            };
            return refreshToken;
        }
        private void SetRefreshToken(RefreshToken newrefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newrefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newrefreshToken.Token, cookieOptions);
            user.RefreshToken = newrefreshToken.Token;
            user.TokenCreated = newrefreshToken.Created;
            user.TokenExpires = newrefreshToken.Expires;
        }
        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
