using Ecommerce.Application.DTOs;

namespace Ecommerce.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AuthResponseDto user);
    }
}