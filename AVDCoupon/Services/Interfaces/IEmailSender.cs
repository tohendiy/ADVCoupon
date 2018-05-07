using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AVDCoupon.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        Task PostMessage(List<string> role, string message, string subject);
    }
}
