using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AVDCoupon.Models;
namespace ADVCoupon.Models
{
    public class Provider
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public byte[] LogoImage { get; set; }
        public List<ApplicationUser> RetailUsers { get; set; }

        public List<Coupon> Coupons { get; set; }
    }
}
