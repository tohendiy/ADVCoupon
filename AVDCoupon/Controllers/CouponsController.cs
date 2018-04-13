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

namespace ADVCoupon.Controllers
{
    public class CouponsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CouponsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coupons
        public async Task<IActionResult> Index()
        {
            var coupons = await _context.Coupons.ToListAsync();
            var couponsListViewModel = new List<CouponEditItemViewModel>(coupons.Count);
            couponsListViewModel = coupons.Select(item => new CouponEditItemViewModel
            {
                CouponGuid = item.CouponGuid,
                CouponName = item.CouponName,
                CouponImage = item.CouponImage,
                TotalCapacity = item.TotalCapacity,
                CurrentCapacity = item.CurrentCapacity
            }).ToList();
            return View(couponsListViewModel);
        }

        // GET: Coupons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .SingleOrDefaultAsync(m => m.CouponGuid == id);
            if (coupon == null)
            {
                return NotFound();
            }
            var couponViewModel = new CouponEditItemViewModel
            {
                CouponGuid = coupon.CouponGuid,
                CouponName = coupon.CouponName,
                CouponImage = coupon.CouponImage,
                TotalCapacity = coupon.TotalCapacity,
                CurrentCapacity = coupon.CurrentCapacity
            };
            return View(couponViewModel);
        }

        // GET: Coupons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponItemViewModel couponItem)
        {
            if (ModelState.IsValid)
            {
                var coupon = new Coupon
                {
                    TotalCapacity = couponItem.TotalCapacity,
                    CurrentCapacity = couponItem.CurrentCapacity,
                    CouponImage = couponItem.CouponImage,
                    CouponName = couponItem.CouponName,
                    CouponGuid = Guid.NewGuid()
                };
                _context.Add(coupon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(couponItem);
        }

        // GET: Coupons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons.SingleOrDefaultAsync(m => m.CouponGuid == id);
            if (coupon == null)
            {
                return NotFound();
            }
            var couponViewModel = new CouponEditItemViewModel
            {
                CouponGuid = coupon.CouponGuid,
                CouponName = coupon.CouponName,
                CouponImage = coupon.CouponImage,
                TotalCapacity = coupon.TotalCapacity,
                CurrentCapacity = coupon.CurrentCapacity
            };
            return View(couponViewModel);
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, CouponEditItemViewModel couponItem)
        {
            if (id != couponItem.CouponGuid)
            {
                return NotFound();
            }
           

            if (ModelState.IsValid)
            {
                try
                {
                    var coupon = new Coupon
                    {
                        TotalCapacity = couponItem.TotalCapacity,
                        CurrentCapacity = couponItem.CurrentCapacity,
                        CouponName = couponItem.CouponName,
                        CouponImage = couponItem.CouponImage,
                        CouponGuid = couponItem.CouponGuid
                    };
                    _context.Update(coupon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponExists(couponItem.CouponGuid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(couponItem);
        }

        // GET: Coupons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coupon = await _context.Coupons
                .SingleOrDefaultAsync(m => m.CouponGuid == id);
            if (coupon == null)
            {
                return NotFound();
            }
            var couponViewModel = new CouponEditItemViewModel
            {
                CouponGuid = coupon.CouponGuid,
                CouponName = coupon.CouponName,
                CouponImage = coupon.CouponImage,
                TotalCapacity = coupon.TotalCapacity,
                CurrentCapacity = coupon.CurrentCapacity
            };
            return View(couponViewModel);
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var coupon = await _context.Coupons.SingleOrDefaultAsync(m => m.CouponGuid == id);
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponExists(Guid id)
        {
            return _context.Coupons.Any(e => e.CouponGuid == id);
        }
    }
}
