using EchoTrack.Api.Data;
using EchoTrack.Api.DTOs;
using EchoTrack.Api.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EchoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null)
                return Unauthorized("Invalid credentials");

            var isValid = PasswordHasher.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                return Unauthorized("Invalid credentials");

            return Ok(new
            {
                message = "Login successful",
                userId = user.Id,
                role = user.Role
            });
        }
    }
}
