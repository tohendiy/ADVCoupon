using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AVDCoupon.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace ADVCoupon.Models
{
    public class Network
    {
        [Key]
        public Guid Id { get; set; }

        public string Caption { get; set; }

        public byte[] LogoImage { get; set; }

        public ProductCategory ProductCategory { get; set; }

        public List<ApplicationUser> MerchantUsers { get; set; }

        public List<Coupon> Coupons { get; set; }

        public List<NetworkPoint> NetworkPoints { get; set; }
    }
}
