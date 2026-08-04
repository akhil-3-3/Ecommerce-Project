using BCrypt.Net;
using Dapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Ecommerce.Infrastructure.Repositories
{
    public class AuthRepository : ConnectionRepository, IAuthRepository
    {
        public AuthRepository(IConfiguration configuration)
            : base(configuration)
        {
        }


        // LOCAL REGISTER
        public async Task<int> RegisterAsync(RegisterDto dto)
        {
            using var connection = GetConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_RegisterUser",
               new
               {
                   dto.UserName,
                   dto.Email,
                   PasswordHash = dto.Password,
                   LoginProvider = "Local",
                   IsVerified = true,
                   VerificationCode = (string?)null
               },
                commandType: CommandType.StoredProcedure);
        }



        // LOGIN
        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            using var connection = GetConnection();

            var user = await connection.QueryFirstOrDefaultAsync<AuthResponseDto>(
                "sp_LoginUser",
                new
                {
                    dto.Email
                },
                commandType: CommandType.StoredProcedure);


            if (user == null)
                return null;


            // Google users cannot login with password
            if (user.LoginProvider != "Local")
    return null;


            if (!BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash))
                return null;


            return user;
        }



        // GET USER BY EMAIL
        public async Task<AuthResponseDto?> GetUserByEmailAsync(string email)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<AuthResponseDto>(
                @"SELECT 
                    UserId,
                    UserName,
                    Email,
                    PasswordHash,
                    LoginProvider,
                    IsVerified
                  FROM Users
                  WHERE Email = @Email",
                new
                {
                    Email = email
                });
        }


public async Task<int> RegisterSocialUserAsync(
    string userName,
    string email,
    string provider)
{
    using var connection = GetConnection();

    // Make username unique
    string baseUserName = userName.Replace(" ", "").ToLower();
    string newUserName = baseUserName;

    int count = 1;

    while (await connection.ExecuteScalarAsync<int>(
        "SELECT COUNT(*) FROM Users WHERE UserName = @UserName",
        new { UserName = newUserName }) > 0)
    {
        newUserName = $"{baseUserName}{count}";
        count++;
    }

    string randomPassword =
        BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString());

            return await connection.ExecuteScalarAsync<int>(
                "sp_RegisterUser",
                new
                {
                    UserName = newUserName,
                    Email = email,
                    PasswordHash = randomPassword,
                    LoginProvider = provider,
                    IsVerified = true,
                    VerificationCode = (string?)null
                },
                commandType: CommandType.StoredProcedure);
            }
    }
}