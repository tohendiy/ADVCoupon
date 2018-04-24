using ADVCoupon.Models;
using AVDCoupon.Data;
using AVDCoupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.Services
{
    public interface ICouponService
    {
        Task<List<Coupon>> GetCoupons(ApplicationDbContext context);
        UserCoupon GetUserCoupon(ApplicationDbContext context, string userId, Guid couponId);

        Task<Coupon> GetCouponById(ApplicationDbContext context, Guid id);
        
    }
}
