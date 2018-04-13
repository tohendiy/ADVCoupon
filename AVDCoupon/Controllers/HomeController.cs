using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AVDCoupon.Models;
using ADVCoupon.Services;
using AVDCoupon.Data;

namespace AVDCoupon.Controllers
{
    public class HomeController : Controller
    {
        ICouponService _couponService;
        ApplicationDbContext _context;

        public HomeController(ICouponService couponService, ApplicationDbContext context)
        {
            _couponService = couponService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var test = await _couponService.GetCoupons(_context);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
