using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AVDCoupon.Models;
using ADVCoupon.Models;

namespace AVDCoupon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserCoupon>()
                   .HasKey(uc => new { uc.UserId, uc.CouponId });
            builder.Entity<UserCoupon>()
                   .HasOne(uc => uc.Coupon)
                   .WithMany(uc => uc.UserCoupons)
                   .HasForeignKey(uc => uc.CouponId);

            builder.Entity<UserCoupon>()
                   .HasOne(uc => uc.ClientUser)
                   .WithMany(uc => uc.UserCoupons)
                   .HasForeignKey(uc => uc.UserId);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
