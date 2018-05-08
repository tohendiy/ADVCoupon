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
using AVDCoupon.Services;
using ADVCoupon.ViewModel.MailViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ADVCoupon.Controllers
{
    public class MailController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MailController(IConfiguration configuration, RoleManager<IdentityRole> roleManager, IEmailSender emailSender)
        {
            _configuration = configuration;
            _roleManager = roleManager;
            _emailSender = emailSender;
        }


        public async Task<IActionResult> Index()
        {
            var mailModel = new MailViewModel();
            var roles = _roleManager.Roles.Select(item=>item.Name).ToList();
            mailModel.RecipientsRoles = new MultiSelectList(roles, "Name");
            return View(mailModel);
        }

       
        public async Task<IActionResult> PostMessage(MailViewModel mailModel)
        {
            await _emailSender.PostMessage(mailModel.Roles.ToList(), mailModel.Message, mailModel.Subject);
            return RedirectToAction("Index");
        }
    }
}