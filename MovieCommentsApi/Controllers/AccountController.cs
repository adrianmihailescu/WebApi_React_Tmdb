using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MovieCommentsApi.Models;
using Microsoft.Extensions.Configuration;

namespace MovieCommentsApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        // Constructor to inject the configuration
        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: api/account/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            Console.WriteLine($"Login attempt with username: {model.Username} and password: {model.Password}");
            // Authenticate the user
            if (model.Username == "admin" && model.Password == "password")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "admin")
                };

                // Create the JWT token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );

                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(new { Token = jwt });
            }

            return Unauthorized();  // Return unauthorized if credentials are incorrect
        }
    }
}
