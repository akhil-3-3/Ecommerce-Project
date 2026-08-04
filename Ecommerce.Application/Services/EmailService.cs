using Ecommerce.Application.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Ecommerce.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendVerificationEmailAsync(string email, string code)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Fragrance Store", _settings.Email));

            if (!MailAddress.TryCreate(email, out _))
            {
                throw new Exception("Invalid email address.");
            }

            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Email Verification";

            message.Body = new TextPart("html")
            {
                Text =
                $"""
                <h2>Email Verification</h2>

                <p>Your verification code is:</p>

                <h1>{code}</h1>

                <p>This code expires in 10 minutes.</p>
                """
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _settings.Host,
                _settings.Port,
                MailKit.Security.SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _settings.Email,
                _settings.Password);

            await client.SendAsync(message);

            await client.DisconnectAsync(true);
        }
    }
}