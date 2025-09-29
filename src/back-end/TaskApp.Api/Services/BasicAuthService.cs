using Microsoft.Extensions.Options;

namespace TaskApp.Api.Services
{
    public class BasicAuthService
    {
        private readonly IConfiguration _configuration;

        public BasicAuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool ValidateCredentials(string username, string password)
        {
            var configUsername = _configuration["Authentication:Username"];
            var configPassword = _configuration["Authentication:Password"];

            return username == configUsername && password == configPassword;
        }
    }
}
