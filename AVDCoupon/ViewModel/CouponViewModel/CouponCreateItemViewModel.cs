using System;
using System.Collections.Generic;
using ADVCoupon.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponCreateItemViewModel
    {
        public Guid Id { get; set; }

        public string Caption { get; set; }

        public byte[] ImageView { get; set; }

        public IFormFile Image { get; set; }

        public double DiscountPercentage { get; set; }

        public double DiscountAbsolute { get; set; }

        [DateLessThan("EndDate", ErrorMessage = "Not valid")]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        public bool IsApproved { get; set; }

        #region Product
        public IEnumerable<Guid> ProductsId { get; set; }


        public MultiSelectList Products { get; set; }


        public bool IsAbsoluteDiscount { get; set; }

        #endregion

    }
}
