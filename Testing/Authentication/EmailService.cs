using System.Net.Mail;
using System.Net;


namespace Testing.Authentication
{
    
        public class EmailService
        {
            private readonly SmtpClient _smtpClient;
            private readonly string _fromAddress;

            public EmailService(IConfiguration configuration)
            {
                _smtpClient = new SmtpClient(configuration["Smtp:Host"], int.Parse(configuration["Smtp:Port"]))
                {
                    Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
                    EnableSsl = bool.Parse(configuration["Smtp:EnableSsl"]),
                };

                _fromAddress = configuration["Smtp:FromAddress"];
            }

            public async Task SendEmailAsync(string to, string subject, string body)
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromAddress),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(to);
                await _smtpClient.SendMailAsync(mailMessage);
            }
        }
    }

