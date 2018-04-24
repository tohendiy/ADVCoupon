using ADVCoupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.PdfViewModel
{
    public class CouponPdfViewModel
    {
        public string Caption { get; set; }
        public double? DiscountAbsolute { get; set; }
        public double? DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Product Product { get; set; }
        public List<NetworkCoupon> NetworkCoupons { get; set; }
        public string BarcodeLink { get; set; }
        public string Barcode { get; set; }
    }
}

