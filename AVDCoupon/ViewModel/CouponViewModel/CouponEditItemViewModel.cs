using System;
using System.ComponentModel.DataAnnotations;
using ADVCoupon.Models;
using Microsoft.AspNetCore.Http;

namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponEditItemViewModel
    {
        public Guid Id { get; set; }

        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        public IFormFile CouponImage { get; set; }

        public byte[] CouponViewImage { get; set; }


        [Required]
        public string CouponName { get; set; }

    }
}
