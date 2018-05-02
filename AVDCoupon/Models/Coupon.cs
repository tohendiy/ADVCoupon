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
        public string Caption { get; set; }
        public double? DiscountAbsolute { get; set; }
        public double? DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalCapacity { get; set; }
        public int CurrentCapacity { get; set; }
        public byte[] Image { get; set; }

        public List<Product> Products { get; set; }
        public List<NetworkCoupon> NetworkCoupons { get; set; }

        public List<UserCoupon> UserCoupons { get; set; }
        public bool IsApproved { get; set; }
        public bool IsAbsoluteDiscount { get; set; }

    }
}
