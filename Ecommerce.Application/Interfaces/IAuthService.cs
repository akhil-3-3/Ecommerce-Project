using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);

        Task<bool> VerifyEmailAsync(VerifyEmailDto dto);

        Task<AuthResponseDto?> LoginAsync(LoginDto dto);

        Task<AuthResponseDto> SocialLoginAsync( string userName, string email, string provider);  
  }
}