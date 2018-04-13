using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVDCoupon.Data;
using AVDCoupon.Models;
using Microsoft.EntityFrameworkCore;

namespace ADVCoupon.Services
{
    public class CouponService : ICouponService
    {
        public async Task<List<Coupon>> GetCoupons(ApplicationDbContext context)
        {
            return await context.Coupons.ToListAsync();
        }
    }
}
