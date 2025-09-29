using Microsoft.AspNetCore.Mvc;
using TaskApp.Api.Services;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly BasicAuthService _authService;

        public AuthController(BasicAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public ActionResult<LoginResponseDto> Login(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest(new LoginResponseDto
                {
                    IsValid = false,
                    Message = "Username and password are required"
                });
            }

            var isValid = _authService.ValidateCredentials(loginDto.Username, loginDto.Password);

            return Ok(new LoginResponseDto
            {
                IsValid = isValid,
                Message = isValid ? "Login successful" : "Invalid username or password"
            });
        }
    }
}
