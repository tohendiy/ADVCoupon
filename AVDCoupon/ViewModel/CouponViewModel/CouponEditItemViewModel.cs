using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponEditItemViewModel
    {
        public Guid CouponGuid { get; set; }

        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        public string CouponImage { get; set; }

        [Required]
        public string CouponName { get; set; }

    }
}
