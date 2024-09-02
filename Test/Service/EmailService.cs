//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;

//namespace Test.Service
//{
//    public class EmailService
//    {
//        private readonly IConfiguration _configuration;

//        public EmailService(IConfiguration configuration)
//        {
//            _configuration = configuration;

//        }

//        public async Task SendEmailAsync(string toEmail, string subject, string message)
//        {
//            var smtpClient = new SmtpClient(_configuration["Smtp:Host"])
//            {
//                Port = int.Parse(_configuration["Smtp:Port"]),
//                Credentials = new NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]),
//                EnableSsl = true,

//            };

//            var mailMessage = new MailMessage
//            {
//                From = new MailAddress(_configuration["Smtp:FromEmail"]),
//                Subject = subject,
//                Body = message,
//                IsBodyHtml = true,
//            };
//            mailMessage.To.Add(toEmail);

//            try
//            {
//                await smtpClient.SendMailAsync(mailMessage);
//            }
//            catch (SmtpException smtpEx)
//            {
               
//                Console.WriteLine($"SMTP error: {smtpEx.Message}");
//                throw;
//            }
//            catch (Exception ex)
//            {
             
//                Console.WriteLine($"General error: {ex.Message}");
//                throw;
//            }
//        }

//    }
//}
