using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<int> RegisterAsync(RegisterDto dto);

        Task<AuthResponseDto?> LoginAsync(LoginDto dto);

        Task<AuthResponseDto?> GetUserByEmailAsync(string email);

        Task<int> RegisterSocialUserAsync(
      string userName,
      string email,
      string provider);
    }
}