using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponItemViewModel
    {

        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        public IFormFile CouponImage { get; set; }

        [Required]
        public string CouponName { get; set; }

        public string MerchantUserId { get; set; }

        public SelectList Users { get; set; }
    }
}
