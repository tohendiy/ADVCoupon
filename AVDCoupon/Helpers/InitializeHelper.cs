using AVDCoupon.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.Helpers
{
    public static class InitializeHelper
    {
        public static async Task InitializeRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "_Aa123456";
            if (await roleManager.FindByNameAsync(Constants.ADMIN_ROLE) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.ADMIN_ROLE));
            }
            if (await roleManager.FindByNameAsync(Constants.USER_ROLE) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.USER_ROLE));
            }
            if (await roleManager.FindByNameAsync(Constants.MERCHANT_ROLE) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.MERCHANT_ROLE));
            }
            if (await roleManager.FindByNameAsync(Constants.SUPPLIER_ROLE) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(Constants.SUPPLIER_ROLE));
            }
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin,Constants.ADMIN_ROLE);
                }
            }
        }
    }
}
