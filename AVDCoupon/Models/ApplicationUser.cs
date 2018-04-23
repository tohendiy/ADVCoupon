using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ADVCoupon.Models;

namespace AVDCoupon.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<UserCoupon> UserCoupons { get; set; }
        public Network Network { get; set; }
        public Provider Provider { get; set; }
        // statistics data
        
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public int ChildNumber { get; set; }


    }
}
