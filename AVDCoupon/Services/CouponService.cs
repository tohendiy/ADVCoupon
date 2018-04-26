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
            return await _context.Coupons.FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task DeleteCouponAsync(Guid Id)
        {
            var coupon = await _context.Coupons.SingleOrDefaultAsync(m => m.Id == Id);
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task<Coupon> GetCouponAsync(Guid Id)
        {
            var coupon = await _context.Coupons.Include(item => item.Product).Include(item => item.Product.Provider)
               .SingleOrDefaultAsync(m => m.Id == Id);
            return coupon;
        }

        public async Task<CouponCreateItemViewModel> GetCouponCreateItemViewModelAsync(Guid Id)
        {
            var coupon = await _context.Coupons.Include(item => item.Product).Include(item => item.Product.Provider)
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (coupon == null)
            {
                return null;
            }
            var couponModel = new CouponCreateItemViewModel
            {
                Id = coupon.Id,
                Caption = coupon.Caption,
                DiscountAbsolute = coupon.DiscountAbsolute ?? default,
                DiscountPercentage = coupon.DiscountPercentage ?? default,
                TotalCapacity = coupon.TotalCapacity,
                CurrentCapacity = coupon.CurrentCapacity,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                IsApproved = coupon.IsApproved,
                ProductId = coupon.Product.Id,
                Name = coupon.Product.Name,
                ImageView = coupon.Product.Image,
                ProviderId = coupon.Product.Provider.Id,
                Providers = GetSelectListProviders()
            };
            return couponModel;
        }

        public async Task<List<Coupon>> GetCouponsAsync()
        {
            var coupons = await _context.Coupons.Include(item=>item.Product).Include(item=>item.Product.Provider).ToListAsync();
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
            var coupon = new Coupon
            {
                Caption = couponModel.Caption,
                Id = Guid.NewGuid(),
                DiscountAbsolute = couponModel.DiscountAbsolute,
                DiscountPercentage = couponModel.DiscountPercentage,
                StartDate = couponModel.StartDate,
                EndDate = couponModel.EndDate,
                TotalCapacity = couponModel.TotalCapacity,
                CurrentCapacity = couponModel.CurrentCapacity,
                IsApproved = couponModel.IsApproved,
                NetworkCoupons = new List<NetworkCoupon>(),
                UserCoupons = new List<UserCoupon>(),
                Product = new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = couponModel.Name,
                    Provider = _context.Providers.FirstOrDefault(item => item.Id == couponModel.ProviderId),
                    NetworkBarcodes = new List<NetworkBarcode>()

                }

            };
            if (couponModel.Image != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await couponModel.Image.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        coupon.Product.Image = memoryStream.ToArray();
                    }
                }
            }
            _context.Add(coupon);
            await _context.SaveChangesAsync();
            return coupon;
        }

        public async Task<CouponCreateItemViewModel> GetCouponProductProvidersListItemViewModelAsync()
        {
            var networkModel = new CouponCreateItemViewModel();
            networkModel.Providers = GetSelectListProviders();
            return networkModel;
        }

        private SelectList GetSelectListProviders()
        {
            var providers = _context.Providers.Select(x => new { Id = x.Id, Value = x.Name });

            var providersSelectList = new SelectList(providers, "Id", "Value");
            return providersSelectList;

        }

        public async Task<List<CouponCreateItemViewModel>> GetCouponCreateItemViewModelsAsync()
        {
            var coupons = await GetCouponsAsync();
            var couponsListViewModel = new List<CouponCreateItemViewModel>(coupons.Count);
            couponsListViewModel = coupons.Select(item => new CouponCreateItemViewModel
            {
                Id = item.Id,
                Caption = item.Caption,
                DiscountAbsolute = item.DiscountAbsolute ?? default,
                DiscountPercentage = item.DiscountPercentage ?? default,
                TotalCapacity = item.TotalCapacity,
                CurrentCapacity = item.CurrentCapacity,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                IsApproved = item.IsApproved,
                ProductId = item.Product.Id,
                Name = item.Product.Name,
                ImageView = item.Product.Image,
                ProviderId = item.Product.Provider.Id,
                Providers = GetSelectListProviders()

            }).ToList();
            return couponsListViewModel;
        }

        public async Task UpdateCouponAsync(CouponCreateItemViewModel couponModel)
        {
            var coupon = await GetCouponAsync(couponModel.Id);
            coupon.Caption = couponModel.Caption;
            coupon.Id = couponModel.Id;
            coupon.DiscountAbsolute = couponModel.DiscountAbsolute;
            coupon.DiscountPercentage = couponModel.DiscountPercentage;
            coupon.StartDate = couponModel.StartDate;
            coupon.EndDate = couponModel.EndDate;
            coupon.TotalCapacity = couponModel.TotalCapacity;
            coupon.CurrentCapacity = couponModel.CurrentCapacity;
            coupon.IsApproved = couponModel.IsApproved;
            coupon.Product.Id = couponModel.ProductId;
            coupon.Product.Name = couponModel.Name;
            coupon.Product.Provider = _context.Providers.FirstOrDefault(item => item.Id == couponModel.ProviderId);


            if (couponModel.Image != null)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await couponModel.Image.CopyToAsync(memoryStream);
                    if (memoryStream != null)
                    {
                        coupon.Product.Image = memoryStream.ToArray();
                    }
                }
            }
            _context.Update(coupon);
            await _context.SaveChangesAsync();

        }
    }
}
