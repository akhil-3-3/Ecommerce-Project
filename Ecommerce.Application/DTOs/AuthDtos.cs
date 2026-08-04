namespace Ecommerce.Application.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }


    public class VerifyEmailDto
    {
        public string Email { get; set; } = string.Empty;

        public string VerificationCode { get; set; } = string.Empty;
    }


    public class LoginDto
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }


    public class AuthResponseDto
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public string LoginProvider { get; set; } = string.Empty;
    }


    public class PendingUserDto
    {
        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string VerificationCode { get; set; } = string.Empty;
    }
}