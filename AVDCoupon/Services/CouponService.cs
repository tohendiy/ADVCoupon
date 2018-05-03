﻿using System;
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
            var coupon = await GetCouponAsync(Id);
            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();
        }

        public async Task<Coupon> GetCouponAsync(Guid Id)
        {
            var coupon = await _context.Coupons.Include(item => item.Products)
               .SingleOrDefaultAsync(m => m.Id == Id);
            return coupon;
        }

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
                IsAbsoluteDiscount = coupon.IsAbsoluteDiscount,
                DiscountAbsolute = coupon.DiscountAbsolute ?? default,
                DiscountPercentage = coupon.DiscountPercentage ?? default,
                TotalCapacity = coupon.TotalCapacity,
                CurrentCapacity = coupon.CurrentCapacity,
                StartDate = coupon.StartDate,
                EndDate = coupon.EndDate,
                IsApproved = coupon.IsApproved,
                ProductsId = coupon.Products.Select(item => item.Id),
                ImageView = coupon.Image,
                Products = GetSelectListProducts()
            };
            return couponModel;
        }

        public async Task<List<Coupon>> GetCouponsAsync()
        {
            var coupons = await _context.Coupons.Include(item=>item.Products).ToListAsync();
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
            var coupon = new Coupon
            {
                Caption = couponModel.Caption,
                Id = Guid.NewGuid(),
                IsAbsoluteDiscount = couponModel.IsAbsoluteDiscount,
                DiscountAbsolute = couponModel.DiscountAbsolute,
                DiscountPercentage = couponModel.DiscountPercentage,
                StartDate = couponModel.StartDate,
                EndDate = couponModel.EndDate,
                TotalCapacity = couponModel.TotalCapacity,
                CurrentCapacity = couponModel.CurrentCapacity,
                IsApproved = couponModel.IsApproved,
                NetworkCoupons = new List<NetworkCoupon>(),
                UserCoupons = new List<UserCoupon>(),
                Products = await _context.Products.Where(item => couponModel.ProductsId.Contains(item.Id)).ToListAsync()
            };
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

            couponModel.Networks = new MultiSelectList(_context.Networks.ToList(),"Id","Caption");

            return couponModel;
        }

        public MultiSelectList GetSelectListProducts()
        {
            var products = _context.Products.Select(x => new { Id = x.Id, Value = x.Name });

            var productsSelectList = new MultiSelectList(products, "Id", "Value");
            return productsSelectList;

        }

        public async Task<List<CouponCreateItemViewModel>> GetCouponCreateItemViewModelsAsync()
        {
            var coupons = await GetCouponsAsync();
            var couponsListViewModel = new List<CouponCreateItemViewModel>(coupons.Count);
            couponsListViewModel = coupons.Select(item => new CouponCreateItemViewModel
            {
                Id = item.Id,
                Caption = item.Caption,
                IsAbsoluteDiscount = item.IsAbsoluteDiscount,
                DiscountAbsolute = item.DiscountAbsolute ?? default,
                DiscountPercentage = item.DiscountPercentage ?? default,
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

        public async Task UpdateCouponAsync(CouponCreateItemViewModel couponModel)
        {
            var coupon = await GetCouponAsync(couponModel.Id);
            coupon.Caption = couponModel.Caption;
            coupon.Id = couponModel.Id;
            coupon.IsAbsoluteDiscount = couponModel.IsAbsoluteDiscount;
            coupon.DiscountAbsolute = couponModel.DiscountAbsolute;
            coupon.DiscountPercentage = couponModel.DiscountPercentage;
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
    }
}
