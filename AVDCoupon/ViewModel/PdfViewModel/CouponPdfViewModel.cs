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
        public string DiscountType { get; set; }
        public double Discount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte[] Image { get; set; }
        public List<byte[]> NetworkImages {get;set;} 
        public string BarcodeLink { get; set; }
        
    }
}

