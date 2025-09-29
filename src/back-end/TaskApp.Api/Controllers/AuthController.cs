using Microsoft.AspNetCore.Mvc;
using TaskApp.Api.Services;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	/// <summary>
	///	Authentication endpoints for basic username/password verification.
	/// </summary>
    public class AuthController : ControllerBase
    {
        private readonly BasicAuthService _authService;

		/// <summary>
		///	Creates a new <see cref="AuthController"/>.
		/// </summary>
		/// <param name="authService">Service handling credential validation.</param>
        public AuthController(BasicAuthService authService)
        {
            _authService = authService;
        }

		/// <summary>
		///	Validates user credentials.
		/// </summary>
		/// <param name="loginDto">Username and password.</param>
		/// <returns>200-OK with validation result or 400 if input is invalid.</returns>
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
