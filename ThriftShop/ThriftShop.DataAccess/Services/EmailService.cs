using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace ThriftShop.API
{
    public class EmailService : IEmailService
    {
        public void SendEmail(EmailModel request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("phamkiet2234@gmail.com"));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("phamkiet2234@gmail.com", "unkvgrfotxksuzbx");
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
