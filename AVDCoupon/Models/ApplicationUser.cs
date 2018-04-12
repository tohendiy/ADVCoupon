using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEntities;
using Microsoft.AspNetCore.Identity;

namespace AVDCoupon.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public List<Coupon> UserCoupons { get; set; }

        // statistics data
        
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public int ChildNumber { get; set; }
    }
}
