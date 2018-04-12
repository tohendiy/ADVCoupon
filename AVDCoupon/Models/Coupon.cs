using System;
using System.Collections.Generic;
using System.Text;

namespace AVDCoupon.Models
{
    public class Coupon
    {
        public Guid CouponGuid { get; set; }
        public int Capacity { get; set; }
        public string CouponImage { get; set; }
        public string CouponName { get; set; }
        public ApplicationUser MerchantUser { get; set; }
    }
}
