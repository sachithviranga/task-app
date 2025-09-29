namespace TaskApp.Shared.DTO
{
    public class LoginDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponseDto
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
