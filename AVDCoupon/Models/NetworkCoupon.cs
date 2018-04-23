using System;
using AVDCoupon.Models;

namespace ADVCoupon.Models
{
    public class NetworkCoupon
    {
        public Guid NetworkId { get; set; }

        public Network Network { get; set; }

        public Guid CouponId { get; set; }

        public Coupon Coupon { get; set; }
    }
}
