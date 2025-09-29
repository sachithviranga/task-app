using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TaskApp.Api.Controllers;
using TaskApp.Api.Services;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Tests.Controllers
{
	/// <summary>
	///	Unit tests for <see cref="AuthController"/> verifying login validation outcomes.
	/// </summary>
	public class AuthControllerTests
	{
		/// <summary>
		///	Builds a <see cref="BasicAuthService"/> wired to an in-memory configuration
		/// so we can deterministically validate credentials.
		/// </summary>
		private static BasicAuthService CreateAuthService(string username, string password)
		{
			var inMemorySettings = new Dictionary<string, string?>
			{
				{"Authentication:Username", username},
				{"Authentication:Password", password}
			};
			var configuration = new ConfigurationBuilder()
				.AddInMemoryCollection(inMemorySettings)
				.Build();
			return new BasicAuthService(configuration);
		}

		/// <summary>
		///	Missing username/password should result in 400-BadRequest with an error payload.
		/// </summary>
		[Fact]
		public void Login_ReturnsBadRequest_WhenMissingCredentials()
		{
			// Arrange
			var service = CreateAuthService("admin", "pass");
			var controller = new AuthController(service);

			// Act
			var result = controller.Login(new LoginDto { Username = "", Password = "" });

			// Assert
			Assert.IsType<BadRequestObjectResult>(result.Result);
		}

		/// <summary>
		///	Valid credentials should produce a 200-OK with IsValid=true.
		/// </summary>
		[Fact]
		public void Login_ReturnsOk_WithValidCredentials()
		{
			// Arrange
			var service = CreateAuthService("admin", "pass");
			var controller = new AuthController(service);

			// Act
			var result = controller.Login(new LoginDto { Username = "admin", Password = "pass" });

			// Assert
			var ok = Assert.IsType<OkObjectResult>(result.Result);
			var payload = Assert.IsType<LoginResponseDto>(ok.Value);
			Assert.True(payload.IsValid);
		}

		/// <summary>
		///	Invalid credentials should produce a 200-OK with IsValid=false (controller always returns 200 for attempted login).
		/// </summary>
		[Fact]
		public void Login_ReturnsOk_WithInvalidCredentials()
		{
			// Arrange
			var service = CreateAuthService("admin", "pass");
			var controller = new AuthController(service);

			// Act
			var result = controller.Login(new LoginDto { Username = "admin", Password = "wrong" });

			// Assert
			var ok = Assert.IsType<OkObjectResult>(result.Result);
			var payload = Assert.IsType<LoginResponseDto>(ok.Value);
			Assert.False(payload.IsValid);
		}
	}
}


