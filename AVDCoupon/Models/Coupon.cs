using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ADVCoupon.Models;

namespace AVDCoupon.Models
{
    public class Coupon
    {
        [Key]
        public Guid CouponGuid { get; set; }
        public int TotalCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public byte[] CouponImage { get; set; }
        public string CouponName { get; set; }
        public ApplicationUser MerchantUser { get; set; }

        public List<UserCoupon> UserCoupons { get; set; }
    }
}
