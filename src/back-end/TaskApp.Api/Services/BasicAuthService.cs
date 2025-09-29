using Microsoft.Extensions.Options;

namespace TaskApp.Api.Services
{
	/// <summary>
	///	Validates credentials against values provided in configuration under
	/// "Authentication:Username" and "Authentication:Password".
	/// </summary>
	public class BasicAuthService
    {
        private readonly IConfiguration _configuration;

		/// <summary>
		///	Creates a new <see cref="BasicAuthService"/> that reads from the given configuration.
		/// </summary>
		/// <param name="configuration">Application configuration.</param>
        public BasicAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

		/// <summary>
		///	Validates the provided username and password against configured values.
		/// </summary>
		/// <param name="username">User name to validate.</param>
		/// <param name="password">Password to validate.</param>
		/// <returns>True if credentials match configuration; otherwise false.</returns>
        public bool ValidateCredentials(string username, string password)
        {
            var configUsername = _configuration["Authentication:Username"];
            var configPassword = _configuration["Authentication:Password"];

            return username == configUsername && password == configPassword;
        }
    }
}
