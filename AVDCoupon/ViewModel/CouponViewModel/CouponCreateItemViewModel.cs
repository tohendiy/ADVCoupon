using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponCreateItemViewModel
    {
        public Guid Id { get; set; }

        public string Caption { get; set; }

        public double DiscountPercentage { get; set; }

        public double DiscountAbsolute { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        public bool IsApproved { get; set; }

        #region Product
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public byte[] ImageView { get; set; }

        public IFormFile Image { get; set; }

        public Guid ProviderId { get; set; }

        public SelectList Providers { get; set; }

        #endregion

    }
}
