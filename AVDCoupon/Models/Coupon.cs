using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ADVCoupon.Models;

namespace AVDCoupon.Models
{
    public class Coupon
    {
        [Key]
        public Guid Id { get; set; }

        public int TotalCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public byte[] CouponImage { get; set; }
        public string CouponName { get; set; }
        public double? DiscountAbsolute { get; set; }
        public double? DiscountPercentage { get; set; }

        public string BarCode { get; set; }

        public bool IsApproved { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Network> Networks { get; set; }

        public ProductCategory ParentProductCategory { get; set; }

        public List<UserCoupon> UserCoupons { get; set; }

    }
}
