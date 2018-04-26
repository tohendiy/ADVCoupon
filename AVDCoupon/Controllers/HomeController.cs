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
            var test1 = await ADVCoupon.Helpers.GeocodingHelper.GetCoordinatesByAddressAsync(new ADVCoupon.Models.Geoposition()
            {
                Country="Україна",
                City="Заболотів",
                Street="Михайла Грушевського",
                Building="32"
            });
            var test = await _couponService.GetCoupons(_context);
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
