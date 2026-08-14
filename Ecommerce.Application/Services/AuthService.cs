using BCrypt.Net;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly RedisService _redisService;
        private readonly IEmailService _emailService;

        public AuthService(
            IAuthRepository authRepository,
            RedisService redisService,
            IEmailService emailService)
        {
            _authRepository = authRepository;
            _redisService = redisService;
            _emailService = emailService;
        }

        // ============================
        // EMAIL REGISTRATION
        // ============================

        public async Task<bool> RegisterAsync(RegisterDto dto)
        {
            // 1. Check if email already exists
            var existingUser =
                await _authRepository.GetUserByEmailAsync(dto.Email);

            // 2. If user exists, stop registration
            if (existingUser != null)
            {
                return false;
            }

            // 3. Generate verification code
            var code = new Random()
                .Next(100000, 999999)
                .ToString();

            // 4. Hash password
            var passwordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // 5. Store pending user in Redis
            var pendingUser = new PendingUserDto
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = passwordHash,
                VerificationCode = code
            };

            await _redisService.SetPendingUserAsync(
                dto.Email,
                pendingUser);

            // 6. Send verification email
            await _emailService.SendVerificationEmailAsync(
                dto.Email,
                code);

            return true;
        }

        // ============================
        // VERIFY EMAIL
        // ============================

        public async Task<bool> VerifyEmailAsync(
            VerifyEmailDto dto)
        {
            var pendingUser =
                await _redisService.GetPendingUserAsync(dto.Email);

            if (pendingUser == null)
                return false;

            if (pendingUser.VerificationCode != dto.VerificationCode)
                return false;

            // The password is ALREADY BCrypt hashed.
            // Do NOT hash it again.
            var userId = await _authRepository.RegisterAsync(
                new RegisterDto
                {
                    UserName = pendingUser.UserName,
                    Email = pendingUser.Email,
                    Password = pendingUser.PasswordHash
                });

            if (userId <= 0)
                return false;

            await _redisService.RemovePendingUserAsync(dto.Email);

            return true;
        }

        // ============================
        // NORMAL LOGIN
        // ============================

        public async Task<AuthResponseDto?> LoginAsync(
            LoginDto dto)
        {
            return await _authRepository.LoginAsync(dto);
        }

        // ============================
        // GOOGLE / FACEBOOK LOGIN
        // ============================

        public async Task<AuthResponseDto> SocialLoginAsync(
            string userName,
            string email,
            string provider)
        {
            var user =
                await _authRepository.GetUserByEmailAsync(email);

            if (user != null)
                return user;

            await _authRepository.RegisterSocialUserAsync(
                userName,
                email,
                provider);

            return await _authRepository.GetUserByEmailAsync(email)
                ?? throw new Exception("Social login failed.");
        }
    }
}