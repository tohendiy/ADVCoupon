using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AVDCoupon.Models;
using ADVCoupon.Models;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading;
using System.Linq.Expressions;

namespace AVDCoupon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Geoposition> Geopositions { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<NetworkBarcode> NetworkBarcodes { get; set; }
        public DbSet<NetworkPoint> NetworkPoints { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Provider> Providers { get; set; }



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


            builder.Entity<NetworkCoupon>()
                   .HasKey(uc => new { uc.NetworkId, uc.CouponId });
            builder.Entity<NetworkCoupon>()
                   .HasOne(uc => uc.Coupon)
                   .WithMany(uc => uc.NetworkCoupons)
                   .HasForeignKey(uc => uc.CouponId);

            builder.Entity<NetworkCoupon>()
                   .HasOne(uc => uc.Network)
                   .WithMany(uc => uc.NetworkCoupons)
                   .HasForeignKey(uc => uc.NetworkId);

			foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // 1. Add the IsDeleted property
                entityType.GetOrAddProperty("IsDeleted", typeof(bool));

                // 2. Create the query filter

                var parameter = Expression.Parameter(entityType.ClrType);

                // EF.Property<bool>(post, "IsDeleted")
                var propertyMethodInfo = typeof(EF).GetMethod("Property").MakeGenericMethod(typeof(bool));
                var isDeletedProperty = Expression.Call(propertyMethodInfo, parameter, Expression.Constant("IsDeleted"));

                // EF.Property<bool>(post, "IsDeleted") == false
                BinaryExpression compareExpression = Expression.MakeBinary(ExpressionType.Equal, isDeletedProperty, Expression.Constant(false));

                // post => EF.Property<bool>(post, "IsDeleted") == false
                var lambda = Expression.Lambda(compareExpression, parameter);

                builder.Entity(entityType.ClrType).HasQueryFilter(lambda);
            }
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

		private void OnBeforeSaving()
		{
			foreach (var entry in ChangeTracker.Entries())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.CurrentValues["IsDeleted"] = false;
						break;

					case EntityState.Deleted:
						entry.State = EntityState.Modified;
						entry.CurrentValues["IsDeleted"] = true;
						break;
				}
			}
		}
    }

    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql("Host=advcoupon.postgres.database.azure.com;Database=advcoupon;Username=adminuser@advcoupon;Password=,thtu123");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
