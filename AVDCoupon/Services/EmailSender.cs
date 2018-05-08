using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using ADVCoupon.Helpers;
using MailKit.Net.Smtp;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace AVDCoupon.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public EmailSender(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
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

        public async Task PostMessage(List<string> role, string message, string subject)
        {
            var users = new List<ApplicationUser>();

            foreach (var r in role)
            {
                users.AddRange(await _userManager.GetUsersInRoleAsync(r));
            }

            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);

            var from = new EmailAddress(Constants.SMTP_EMAIL_FROM, Constants.SMTP_EMAIL_NAME);
            List<EmailAddress> tos = new List<EmailAddress>();
            foreach (var u in users)
            {
                tos.Add(new EmailAddress(u.Email));
            }
            var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, message, "", displayRecipients);
            var response = await client.SendEmailAsync(msg);

        }
    }
}
