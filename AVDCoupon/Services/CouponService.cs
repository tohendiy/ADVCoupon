using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.CouponViewModel;
using AVDCoupon.Data;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ADVCoupon.Helpers;

namespace ADVCoupon.Services
{
    public class CouponService : ICouponService
    {

        private ApplicationDbContext _context;
        public CouponService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Coupon> GetCouponById(Guid id)
        {
            return await _context.Coupons.Include("NetworkBarcodes.Networks").FirstOrDefaultAsync(x => x.Id == id);
        }



        public UserCoupon GetUserCoupon(string userId, Guid couponId)
        {

            var userCoupon = from coupon in _context.Coupons
                             where coupon.UserCoupons.FirstOrDefault(y => y.CouponId == couponId && y.UserId == userId) != null
                             select coupon.UserCoupons.FirstOrDefault(y => y.CouponId == couponId && y.UserId == userId);

            return userCoupon.FirstOrDefault();
        }



        public async Task<Coupon> CreateCouponAsync(Coupon coupon)
        {
            coupon.Id = Guid.NewGuid();
            _context.Add(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }
        /// <summary>
        /// Deletes the coupon async.
        /// </summary>
        /// <returns>The coupon async.</returns>
        /// <param name="Id">Identifier.</param>
        public async Task DeleteCouponAsync(Guid Id)
        {
            var coupon = await GetCouponAsync(Id);
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Gets the coupon async.
        /// </summary>
        /// <returns>The coupon async.</returns>
        /// <param name="Id">Identifier.</param>
        public async Task<Coupon> GetCouponAsync(Guid Id)
        {
            var coupon = await _context.Coupons.Include(item => item.Products)
               .SingleOrDefaultAsync(m => m.Id == Id);
            return coupon;
        }
        /// <summary>
        /// Gets the coupon create item view model async.
        /// </summary>
        /// <returns>The coupon create item view model async.</returns>
        /// <param name="Id">Identifier.</param>
        public async Task<CouponCreateItemViewModel> GetCouponCreateItemViewModelAsync(Guid Id)
        {
            var coupon = await _context.Coupons.Include(item => item.Products)
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (coupon == null)
            {
                return null;
            }
            var couponModel = new CouponCreateItemViewModel
            {
                Id = coupon.Id,
                Caption = coupon.Caption,
                DiscountTypeText = coupon.DiscountType,
                Discount = coupon.Discount,
                TotalCapacity = coupon.TotalCapacity,
                CurrentCapacity = coupon.CurrentCapacity,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                IsApproved = coupon.IsApproved,
                ProductsId = coupon.Products.Select(item => item.Id),
                ImageView = coupon.Image,
                Products = GetSelectListProducts(),
                DiscountType = GetSelectListDiscountTypes()
            };
            return couponModel;
        }

        public async Task<List<Coupon>> GetCouponsAsync()
        {
            var coupons = await _context.Coupons.Include(item => item.Products).ToListAsync();
            return coupons;
        }

        public async Task<List<Coupon>> GetCouponsByUserAsync(string id)
        {
            var coupons = await _context.Coupons.Where(item => item.UserCoupons.Any(item1 => item1.UserId == id)).ToListAsync();
            return coupons;
        }

        public async Task<List<Coupon>> GetCouponsByNetworkAsync(Guid id)
        {
            var coupons = await _context.Coupons.Where(item => item.NetworkCoupons.Any(item1 => item1.NetworkId == id)).ToListAsync();
            return coupons;
        }

        public bool IsExist(Guid Id)
        {
            return _context.Coupons.Any(e => e.Id == Id);
        }

        public async Task UpdateCouponAsync(Coupon coupon)
        {
            _context.Update(coupon);
            await _context.SaveChangesAsync();
        }


        public async Task<Coupon> CreateCouponAsync(CouponCreateItemViewModel couponModel)
        {
            var networkBarcodes = from c in couponModel.NetworkBarcodes
                                  select new NetworkBarcode()
                                  {
                                      BarcodeValue = c.BarcodeValue,
                                      BarcodeType = c.BarcodeType,
                                      Id = Guid.NewGuid(),
                                      Networks = _context.Networks.Where(x => c.Networks.Contains(x.Id)).ToList()
                                  };
            var networkCoupons = networkBarcodes.SelectMany(item => item.Networks).ToList();

            var coupon = new Coupon
            {
                Caption = couponModel.Caption,
                Id = Guid.NewGuid(),
                Discount = couponModel.Discount,
                DiscountType = couponModel.DiscountTypeText,
                StartDate = couponModel.StartDate,
                EndDate = couponModel.EndDate,
                TotalCapacity = couponModel.TotalCapacity,
                CurrentCapacity = couponModel.CurrentCapacity,
                IsApproved = couponModel.IsApproved,
                NetworkCoupons = new List<NetworkCoupon>(),
                NetworkBarcodes = networkBarcodes.ToList(),
                UserCoupons = new List<UserCoupon>(),
                Products = await _context.Products.Where(item => couponModel.ProductsId.Contains(item.Id)).ToListAsync()
            };

            var networkCouponsList = new List<NetworkCoupon>(networkCoupons.Count);

            networkCouponsList = networkCoupons.Select(item => new NetworkCoupon
            {
                CouponId = coupon.Id,
                Coupon = coupon,
                NetworkId = item.Id,
                Network = item

            }).ToList();

            coupon.NetworkCoupons = networkCouponsList;

            if (couponModel.Image != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await couponModel.Image.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        coupon.Image = memoryStream.ToArray();
                    }
                }
            }
            _context.Add(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }
        
        public async Task<CouponCreateItemViewModel> GetCouponProductsListItemViewModelAsync()
        {
            var couponModel = new CouponCreateItemViewModel();
            couponModel.Products = GetSelectListProducts();
            couponModel.NetworkBarcodes = new List<NetworkBarcodeViewModel>()
            {
                new NetworkBarcodeViewModel()
                {
                    BarcodeValue = "",
                    Networks = new List<Guid>()
                }
            };

            couponModel.Networks = GetMultiSelectListNetworks();
            couponModel.DiscountType = GetSelectListDiscountTypes();
            return couponModel;
        }
        
        public MultiSelectList GetSelectListProducts()
        {
			var products = _context.Products.Include(item=> item.Provider).Select(x => new { Id = x.Id, Value = x.Name, Group = x.Provider.Name });

			var productsSelectList = new MultiSelectList(products, "Id", "Value", null, "Group");
            return productsSelectList;

        }

        public SelectList GetSelectListDiscountTypes()
        {
            string[] discountTypes = { Constants.DISCOUNT_TYPE_PERCENT, Constants.DISCOUNT_TYPE_ABSOLUTE };
            var discountSelectList = new SelectList(discountTypes);
            return discountSelectList;
        }

        public MultiSelectList GetMultiSelectListNetworks()
        {
            var networks = _context.Networks.ToList();
            var networksSelectList = new MultiSelectList(networks, "Id", "Caption");
            return networksSelectList;
        }

        public async Task<List<CouponCreateItemViewModel>> GetCouponCreateItemViewModelsAsync()
        {
            var coupons = await GetCouponsAsync();
            var couponsListViewModel = new List<CouponCreateItemViewModel>(coupons.Count);
            couponsListViewModel = coupons.Select(item => new CouponCreateItemViewModel
            {
                Id = item.Id,
                Caption = item.Caption,
                Discount = item.Discount,
                DiscountTypeText = item.DiscountType,
                TotalCapacity = item.TotalCapacity,
                CurrentCapacity = item.CurrentCapacity,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                IsApproved = item.IsApproved,
                ProductsId = item.Products.Select(element => element.Id),
                ImageView = item.Image,
                Products = GetSelectListProducts(),

            }).ToList();
            return couponsListViewModel;
        }


        public async Task<List<Coupon>> GetOnlyApprovedDateCouponsAsync()
        {
            var coupons = await GetOnlyApprovedDateCouponsQuery().ToListAsync();
            return coupons;
        }

        public async Task<List<Coupon>> GetRelatedCouponsByNetworkAsync(Guid idCoupon, Guid idNetwork)
        {
            var couponsQuery = GetOnlyApprovedDateCouponsQuery();
            var coupons = await couponsQuery.Where(item => item.Id != idCoupon && item.NetworkCoupons.Any(item1 => item1.NetworkId == idNetwork)).ToListAsync();
            return coupons;
        }


        public async Task UpdateCouponAsync(CouponCreateItemViewModel couponModel)
        {
            var coupon = await GetCouponAsync(couponModel.Id);
            coupon.Caption = couponModel.Caption;
            coupon.Id = couponModel.Id;
            coupon.Discount = couponModel.Discount;
            coupon.DiscountType = couponModel.DiscountTypeText;
            coupon.StartDate = couponModel.StartDate;
            coupon.EndDate = couponModel.EndDate;
            coupon.TotalCapacity = couponModel.TotalCapacity;
            coupon.CurrentCapacity = couponModel.CurrentCapacity;
            coupon.IsApproved = couponModel.IsApproved;
            coupon.Products = await _context.Products.Where(item => couponModel.ProductsId.Contains(item.Id)).ToListAsync();


            if (couponModel.Image != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await couponModel.Image.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        coupon.Image = memoryStream.ToArray();
                    }
                }
            }
            _context.Update(coupon);
            await _context.SaveChangesAsync();

        }

        public async Task ActivateCouponAsync(Guid Id)
        {
            var coupon = await GetCouponAsync(Id);
            coupon.IsApproved = true;
            _context.Update(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateCouponAsync(Guid Id)
        {
            var coupon = await GetCouponAsync(Id);
            coupon.IsApproved = false;
            _context.Update(coupon);
            await _context.SaveChangesAsync();
        }

        private IQueryable<Coupon> GetOnlyApprovedDateCouponsQuery()
        {
            var coupons = _context.Coupons.Where(item => item.IsApproved && item.StartDate > DateTime.Now && item.EndDate < DateTime.Now);
            return coupons;
        }


    }
}
