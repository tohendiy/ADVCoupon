using ADVCoupon.Models;
using ADVCoupon.ViewModel.CouponViewModel;
using AVDCoupon.Data;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.Services
{
    public interface ICouponService
    {
        UserCoupon GetUserCoupon(string userId, Guid couponId);
        Task<CouponCreateItemViewModel> GetCouponProductsListItemViewModelAsync();

        Task<Coupon> GetCouponById(Guid id);

        Task<Coupon> GetCouponAsync(Guid Id);
        Task<CouponCreateItemViewModel> GetCouponCreateItemViewModelAsync(Guid Id);
        Task<List<Coupon>> GetRelatedCouponsByNetworkAsync(Guid idCoupon, Guid idNetwork);


        Task<List<Coupon>> GetCouponsAsync();
        Task<List<Coupon>> GetOnlyApprovedDateCouponsAsync();
        Task<List<Coupon>> GetCouponsByUserAsync(string id);
        Task<List<Coupon>> GetCouponsByNetworkAsync(Guid id);
        Task<List<CouponCreateItemViewModel>> GetCouponCreateItemViewModelsAsync();

        Task<Coupon> CreateCouponAsync(Coupon coupon);
        Task<Coupon> CreateCouponAsync(CouponCreateItemViewModel couponModel);

        Task UpdateCouponAsync(Coupon coupon);
        Task UpdateCouponAsync(CouponCreateItemViewModel couponModel);

        Task DeleteCouponAsync(Guid Id);

        Task ActivateCouponAsync(Guid Id);
        Task DeactivateCouponAsync(Guid Id);

        bool IsExist(Guid Id);
        MultiSelectList GetSelectListProducts();
        SelectList GetSelectListDiscountTypes();
        MultiSelectList GetMultiSelectListNetworks();

        
    }
}
