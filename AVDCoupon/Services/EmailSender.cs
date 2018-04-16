using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using ADVCoupon.Helpers;
using MailKit.Net.Smtp;

namespace AVDCoupon.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(Constants.SMTP_EMAIL_NAME, Constants.SMTP_EMAIL_FROM));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(Constants.SMTP_SERVER, Constants.SMTP_PORT, Constants.SMTP_SSL);
                    await client.AuthenticateAsync(Constants.SMTP_EMAIL_FROM, Constants.SMTP_EMAIL_PASSWORD);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);

                }
            }
            catch(Exception ex)
            { }

        }
    }
}
