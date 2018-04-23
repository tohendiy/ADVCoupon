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
    }
}
