﻿using System;
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

                emailMessage.From.Add(new MailboxAddress("ADV Coupon", Constants.EMAIL_NOREPLY_FROM));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    await client.AuthenticateAsync(Constants.EMAIL_NOREPLY_FROM, "A80962432409");
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);

                }
            }
            catch(Exception ex)
            {
                var test = ex;
            }

        }
    }
}
