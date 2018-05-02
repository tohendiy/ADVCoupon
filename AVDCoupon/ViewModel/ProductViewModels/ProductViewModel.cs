using ADVCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.ProductViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
        public string SupplierName { get; set; }
        public string SKU { get; set; }
        public byte[] Image { get; set; }
        public IFormFile ImageFile { get; set; }
        public SelectList Providers { get; set; }
        public Guid ProviderId { get; set; }
    }
}
