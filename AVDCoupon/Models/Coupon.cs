using System;
using System.ComponentModel.DataAnnotations;

namespace AVDCoupon.Models
{
    public class Coupon
    {
        [Key]
        public Guid CouponGuid { get; set; }
        public int Capacity { get; set; }
        public string CouponImage { get; set; }
        public string CouponName { get; set; }
        public ApplicationUser MerchantUser { get; set; }
    }
}
