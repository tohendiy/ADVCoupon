using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AVDCoupon.Data;
using AVDCoupon.Models;
using ADVCoupon.ViewModel.CouponViewModel;
using Microsoft.AspNetCore.Identity;
using System.IO;
using System.Net.Http.Headers;
using ADVCoupon.Services;
using ADVCoupon.Services.Interfaces;

namespace ADVCoupon.Controllers
{
    public class CouponsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICouponService _couponService;
        private readonly ITemplateService _templateService;


        public CouponsController(ApplicationDbContext context, ITemplateService templateService, UserManager<ApplicationUser> userManager, ICouponService couponService)
        {
            _templateService = templateService;
            _context = context;
            _userManager = userManager;
            _couponService = couponService;
        }

        // GET: Coupons
        public async Task<IActionResult> Index()
        {
            //var coupons = await _context.Coupons.ToListAsync();
            //var couponsListViewModel = new List<CouponDetailItemViewModel>(coupons.Count);
            //couponsListViewModel = coupons.Select(item => new CouponDetailItemViewModel
            //{
            //    Id = item.Id,
            //    CouponName = item.CouponName,
            //    TotalCapacity = item.TotalCapacity,
            //    CurrentCapacity = item.CurrentCapacity,
            //    CouponImage = item.CouponImage
            //}).ToList();
            //return View(couponsListViewModel);
            return View();
        }

        // GET: Coupons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var coupon = await _context.Coupons
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (coupon == null)
            //{
            //    return NotFound();
            //}
            //var couponViewModel = new CouponDetailItemViewModel
            //{
            //    Id = coupon.Id,
            //    CouponName = coupon.CouponName,
            //    TotalCapacity = coupon.TotalCapacity,
            //    CurrentCapacity = coupon.CurrentCapacity,
            //    CouponImage = coupon.CouponImage
            //};

            return View();
        }

        // GET: Coupons/Create
        public IActionResult Create()
        {
            var users = _userManager.Users.Select(x => new { Id = x.Id, Value = x.Email });

            var model = new CouponItemViewModel();
            model.Users = new SelectList(users, "Id", "Value");

            return View(model);
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponItemViewModel couponItem)
        {
            //if (ModelState.IsValid)
            //{
                // for test only! AA
            //    var location = Helpers.CookiesHelper.GetGeolocation(HttpContext);

            //    var coupon = new Coupon
            //    {
            //        TotalCapacity = couponItem.TotalCapacity,
            //        CurrentCapacity = couponItem.CurrentCapacity,
            //        CouponName = couponItem.CouponName,
            //        Id = Guid.NewGuid()
            //        //MerchantUser = _userManager.Users.FirstOrDefault(item => item.Id == couponItem.MerchantUserId),
                    
            //    };
            //    using (var memoryStream = new MemoryStream())
            //    {
            //        await couponItem.CouponImage.CopyToAsync(memoryStream);
            //        coupon.CouponImage = memoryStream.ToArray();
            //    }
            //    _context.Add(coupon);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            return View();
        }

        // GET: Coupons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var coupon = await _context.Coupons.SingleOrDefaultAsync(m => m.Id == id);
            //if (coupon == null)
            //{
            //    return NotFound();
            //}
            //var couponViewModel = new CouponEditItemViewModel
            //{
            //    Id = coupon.Id,
            //    CouponName = coupon.CouponName,
            //    TotalCapacity = coupon.TotalCapacity,
            //    CurrentCapacity = coupon.CurrentCapacity,
            //    CouponViewImage = coupon.CouponImage
            //};
            return View();
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CouponEditItemViewModel couponItem)
        {
            //if (id != couponItem.Id)
            //{
            //    return NotFound();
            //}
           

            //if (ModelState.IsValid)
            //{
                //try
                //{
                //    var coupon = new Coupon
                //    {
                //        TotalCapacity = couponItem.TotalCapacity,
                //        CurrentCapacity = couponItem.CurrentCapacity,
                //        CouponName = couponItem.CouponName,
                //        Id = couponItem.Id
                //    };
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        await couponItem.CouponImage.CopyToAsync(memoryStream);
                //        if (memoryStream != null)
                //        {
                //            coupon.CouponImage = memoryStream.ToArray();
                //        }
                //    }

                //    _context.Update(coupon);
                //    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!CouponExists(couponItem.Id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}
                //return RedirectToAction(nameof(Index));
            //}
            return View();
        }

        // GET: Coupons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var coupon = await _context.Coupons
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (coupon == null)
            //{
            //    return NotFound();
            //}
            //var couponViewModel = new CouponDetailItemViewModel
            //{
            //    Id = coupon.Id,
            //    CouponName = coupon.CouponName,
            //    TotalCapacity = coupon.TotalCapacity,
            //    CurrentCapacity = coupon.CurrentCapacity,
            //    CouponImage = coupon.CouponImage
            //};
            return View();
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var coupon = await _context.Coupons.SingleOrDefaultAsync(m => m.Id == id);
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponExists(Guid id)
        {
            return _context.Coupons.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> Grid()
        {
            //var coupons = await _context.Coupons.ToListAsync();
            //var couponsListViewModel = new List<CouponClientGridViewModel>(coupons.Count);
            //couponsListViewModel = coupons.Select(item => new CouponClientGridViewModel
            //{
            //    Id = item.Id,
            //    CouponName = item.CouponName,
            //    CouponImage = item.CouponImage
            //}).ToList();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ClientDetail(Guid? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var coupon = await _context.Coupons
            //    .SingleOrDefaultAsync(m => m.Id == id);
            //if (coupon == null)
            //{
            //    return NotFound();
            //}
            //var couponViewModel = new CouponClientGridViewModel
            //{
            //    Id = coupon.Id,
            //    CouponName = coupon.CouponName,
            //    CouponImage = coupon.CouponImage
            //};

            return View();
        }

        [HttpGet]
        [Route("/pdf/coupon")]
        public async Task<IActionResult> GenerateCoupon(string couponId, string userId)
        {
            var output = await Helpers.PdfGenerator.GeneratePDF(_context, _templateService, _couponService,new Guid(couponId),userId);
            return File(output, "application/pdf");
        }


    }
}
