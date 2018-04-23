using System;
using AVDCoupon.Models;
namespace ADVCoupon.Models
{
    public class UserCoupon
    {
        public string UserId { get; set; }

        public ApplicationUser ClientUser { get; set; }

        public Guid CouponId { get; set; }

        public Coupon Coupon { get; set; }

        public DateTime Created { get; set; }

        public Geoposition Geoposition { get; set; }
    }
}
