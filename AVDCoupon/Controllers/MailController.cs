using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ADVCoupon.Controllers
{
    public class MailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public MailController(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }


        public IActionResult Index()
        {

            return View();
        }

       
        [Route("SendNotification")]
        public async Task<IActionResult> PostMessage(List<string> role, string message, string subject)
        {
            var users = new List<ApplicationUser>();

            foreach(var r in role)
            {
                users.AddRange(await _userManager.GetUsersInRoleAsync(r));
            }

            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            
            var from = new EmailAddress("olecsiuyae@gmail.com", "ADV Team");
            List<EmailAddress> tos = new List<EmailAddress>();
            foreach(var u in users)
            {
                tos.Add(new EmailAddress(u.Email));
            }
            var displayRecipients = false; // set this to true if you want recipients to see each others mail id 
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, message, "", false);
            var response = await client.SendEmailAsync(msg);
            return LocalRedirect("Index");
        }
    }
}