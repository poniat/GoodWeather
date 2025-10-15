using System.Net;
using System.Net.Mail;

namespace  GoodWeather.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string _smtpServer = "smtp.yourserver.com";
        private readonly int _port = 587;
        private readonly string _username = "your@email.com";
        private readonly string _password = "yourpassword";
        private readonly string _from = "your@email.com";
        private readonly string _to = "your@email.com";


        public void Send(string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _port,
                Credentials = new NetworkCredential(_username, _password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_from),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };
            mailMessage.To.Add(_to);

            //smtpClient.Send(mailMessage);
        }
    }
}