using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AVDCoupon.Data;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ADVCoupon.ViewModel.UsersViewModels;
using ADVCoupon.Services;
using ADVCoupon.Helpers;

namespace ADVCoupon.Controllers
{
    [Authorize(Roles = Constants.ADMIN_ROLE)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private readonly IProviderService _providerService;
        private readonly INetworkService _networkService; 

        public UsersController(ApplicationDbContext context,INetworkService networkService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IProviderService providerService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _providerService = providerService;
            _networkService = networkService;
        }

        // GET: Users
        [HttpGet]
        public IActionResult Index()
        {
            //var users = await _userManager.Users.Include(x => x.Provider).Include(y => y.Network).ToListAsync();

            //var usersWithRoles = from u in users
            //                     select new UserTableItemViewModel { User = u, Role = _userManager.GetRolesAsync(u).Result[0] };
            
            //foreach(var t in users)
            //{
            //    var test = await _userManager.GetClaimsAsync(t);
            //}
            
            return View();
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _userManager.Users
                .Include(x => x.Provider).Include(y => y.Network).SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            var role = await _userManager.GetRolesAsync(applicationUser);

            var model = new UserViewModel()
            {
                Name = applicationUser.UserName,
                Email = applicationUser.Email,
                Id = applicationUser.Id,
                Role = role[0],
                Network = applicationUser.Network?.Caption,
                Provider = applicationUser.Provider?.Name

            };

            return View(model);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var model = new UserViewModel()
            {
                Providers = new SelectList(await _providerService.GetProvidersAsync(), "Id", "Name"),
                Networks = new SelectList(await _networkService.GetNetworksAsync(), "Id", "Caption")
            };
            return View(model);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Password,Provider,Network,Role")] UserViewModel applicationUserCreateModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    Email = applicationUserCreateModel.Email,
                    UserName = applicationUserCreateModel.Email
                };

                if (applicationUserCreateModel.Role == Helpers.Constants.MERCHANT_ROLE)
                {
                    user.Network = await _networkService.GetNetwork(new Guid(applicationUserCreateModel.Network));
                }
                else
                {
                    user.Provider = await _providerService.GetProvider(new Guid(applicationUserCreateModel.Provider));
                }

                var response = await _userManager.CreateAsync(user, 
                    applicationUserCreateModel.Password);
                
                await _context.SaveChangesAsync();

                await _userManager.AddToRoleAsync(user, applicationUserCreateModel.Role);
                
                return RedirectToAction(nameof(Index));
            }
            return View(applicationUserCreateModel);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            var model = new UserViewModel()
            {
                Providers = new SelectList(await _providerService.GetProvidersAsync(), "Id", "Name"),
                Networks = new SelectList(await _networkService.GetNetworksAsync(), "Id", "Caption"),
                Name = applicationUser.UserName,
                Email = applicationUser.Email,
                Id = applicationUser.Id

            };

            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Email,Password")] UserViewModel applicationUserCreateModel)
        {
            if (id != applicationUserCreateModel.Id)
            {
                return NotFound();
            }

            var user = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);

            if (ModelState.IsValid)
            {
                try
                {
                    user.UserName = applicationUserCreateModel.Email;
                    user.Email = applicationUserCreateModel.Email;

                    await _userManager.UpdateAsync(user);

                    if (!string.IsNullOrEmpty(applicationUserCreateModel.Password))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        var result = await _userManager.ResetPasswordAsync(user, token, applicationUserCreateModel.Password);
                    }

                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUserCreateModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _userManager.Users
                .Include(x => x.Provider).Include(y => y.Network).SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            var role = await _userManager.GetRolesAsync(applicationUser);

            var model = new UserViewModel()
            {
                Name = applicationUser.UserName,
                Email = applicationUser.Email,
                Id = applicationUser.Id,
                Role = role[0],
                Network = applicationUser.Network?.Caption,
                Provider = applicationUser.Provider?.Name

            };

            return View(model);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
            await _userManager.DeleteAsync(applicationUser);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<PartialViewResult> IndexGrid()
        {
            var users = await _userManager.Users.Include(x => x.Provider).Include(y => y.Network).ToListAsync();

            var usersWithRoles = from u in users
                                 select new UserTableItemViewModel { User = u, Role = _userManager.GetRolesAsync(u).Result[0], NetworkName = u.Network?.Caption, ProviderName = u.Provider?.Name };
            
            // Only grid query values will be available here.
            return PartialView("_IndexGrid", usersWithRoles.ToList());
        }


        private bool ApplicationUserExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }
    }
}
