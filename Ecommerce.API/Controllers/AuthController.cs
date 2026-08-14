using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.RateLimiting;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // ==========================
        // EMAIL REGISTER
        // ==========================
        [EnableRateLimiting("fixed")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result)
            {
                return BadRequest("Email is already registered.");
            }

            return Ok("Verification code sent to your email.");
        }

        // ==========================
        // VERIFY EMAIL
        // ==========================

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto dto)
        {
            var result = await _authService.VerifyEmailAsync(dto);

            if (!result)
                return BadRequest("Invalid or expired verification code.");

            return Ok(new
            {
                Message = "Email verified successfully."
            });
        }

        // ==========================
        // NORMAL LOGIN
        // ==========================
        [EnableRateLimiting("fixed")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authService.LoginAsync(dto);

            if (user == null)
                return Unauthorized("Invalid Email or Password.");

            await SignInUser(user);

            return Ok(new
            {
                Message = "Login Successful"
            });
        }

        // ==========================
        // GOOGLE LOGIN
        // ==========================
        [EnableRateLimiting("fixed")]
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(GoogleResponse))
            };

            return Challenge(
                properties,
                GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(
                GoogleDefaults.AuthenticationScheme);

            if (!result.Succeeded)
                return BadRequest("Google login failed.");

            var email = result.Principal?
                .FindFirst(ClaimTypes.Email)?.Value;

            var name = result.Principal?
                .FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(email))
                return BadRequest("Email not received from Google.");

            var user = await _authService.SocialLoginAsync(
                name ?? email,
                email,
                "Google");

            await SignInUser(user);

            return Redirect("http://localhost:5173/home");
        }

        // ==========================
        // FACEBOOK LOGIN
        // ==========================

        [HttpGet("facebook-login")]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(FacebookResponse))
            };

            return Challenge(
                properties,
                FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet("me")]
public IActionResult GetCurrentUser()
{
    if (!User.Identity?.IsAuthenticated ?? true)
    {
        return Unauthorized();
    }

    return Ok(new
    {
        UserName = User.FindFirst(ClaimTypes.Name)?.Value,
        Email = User.FindFirst(ClaimTypes.Email)?.Value
    });
}

        [HttpGet("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(
                FacebookDefaults.AuthenticationScheme);
            if (!result.Succeeded)
                return BadRequest("Facebook login failed.");

            var email = result.Principal?
                .FindFirst(ClaimTypes.Email)?.Value;

            var name = result.Principal?
                .FindFirst(ClaimTypes.Name)?.Value;

              foreach (var claim in result.Principal!.Claims)
{
    Console.WriteLine($"{claim.Type} : {claim.Value}");
}

            if (string.IsNullOrEmpty(email))
                return BadRequest("Email not received from Facebook.");

            var user = await _authService.SocialLoginAsync(
                name ?? email,
                email,
                "Facebook");

            await SignInUser(user);

            return Redirect("http://localhost:5173/home");
        }

        // ==========================
        // SIGN IN COOKIE
        // ==========================

        private async Task SignInUser(AuthResponseDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.UserId.ToString()),

                new Claim(
                    ClaimTypes.Name,
                    user.UserName),

                new Claim(
                    ClaimTypes.Email,
                    user.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
                });
        }

        // ==========================
        // LOGOUT
        // ==========================

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok(new
            {
                Message = "Logged out successfully."
            });
        }
    }
}