using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using AVDCoupon.Data;
using AVDCoupon.Models;
using Microsoft.EntityFrameworkCore;

namespace ADVCoupon.Services
{
    public class CouponService : ICouponService
    {
        public async Task<Coupon> GetCouponById(ApplicationDbContext context, Guid id)
        {
            return await context.Coupons.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Coupon>> GetCoupons(ApplicationDbContext context)
        {
            return await context.Coupons.ToListAsync();
        }

        public UserCoupon GetUserCoupon(ApplicationDbContext context, string userId, Guid couponId)
        {

            var userCoupon = from coupon in context.Coupons
                       where coupon.UserCoupons.FirstOrDefault(y => y.CouponId == couponId && y.UserId == userId) != null
                       select coupon.UserCoupons.FirstOrDefault(y => y.CouponId == couponId && y.UserId == userId);

            return userCoupon.FirstOrDefault();
        }
    }
}
