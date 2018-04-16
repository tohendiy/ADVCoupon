using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AVDCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponItemViewModel
    {

        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string CouponImage { get; set; }

        [Required]
        public string CouponName { get; set; }

        public string MerchantUserId { get; set; }

        public SelectList Users { get; set; }
    }
}
