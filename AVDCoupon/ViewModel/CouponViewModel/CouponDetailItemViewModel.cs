using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.ViewModel.CouponViewModel
{
    public class CouponDetailItemViewModel
    {
        public Guid Id { get; set; }

        public int TotalCapacity { get; set; }

        public int CurrentCapacity { get; set; }

        public byte[] CouponImage { get; set; }

        [Required]
        public string CouponName { get; set; }

    }
}
