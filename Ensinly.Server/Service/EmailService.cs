using Ensinly.Server.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Ensinly.Server.Service {
    public class EmailService {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings) {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmail(string destinatario, string assunto, string corpo) {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Ensily - Sistema de Ensino Particular", _emailSettings.Username));
            email.To.Add(new MailboxAddress("",destinatario));
            email.Subject = assunto;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
                Text = corpo
            };

            using (var smtp = new SmtpClient()) {
                await smtp.ConnectAsync(_emailSettings.Server, _emailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
