using MailKit.Net.Smtp;
using MimeKit;
using System.Diagnostics;

namespace AppSecAssignment.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // DEMO MODE: Log email to Debug Output instead of sending real email
            // This simulates email sending for safe local testing without SMTP credentials
            
            var emailMessage = $@"
========================================
?? EMAIL NOTIFICATION (DEBUG MODE)
========================================
To: {toEmail}
Subject: {subject}
Message:
{body}
========================================
";

            // Write to Visual Studio Output Window
            Debug.WriteLine(emailMessage);
            
            // Also write to application logs
            _logger.LogInformation(emailMessage);

            // Simulate async operation
            await Task.CompletedTask;

            // Note: In production, replace this with real SMTP:
            // var message = new MimeMessage();
            // message.From.Add(MailboxAddress.Parse(_configuration["Email:From"]));
            // message.To.Add(MailboxAddress.Parse(toEmail));
            // message.Subject = subject;
            // message.Body = new TextPart("plain") { Text = body };
            // using var smtp = new SmtpClient();
            // await smtp.ConnectAsync(...);
            // await smtp.AuthenticateAsync(...);
            // await smtp.SendAsync(message);
            // await smtp.DisconnectAsync(true);
        }
    }
}
