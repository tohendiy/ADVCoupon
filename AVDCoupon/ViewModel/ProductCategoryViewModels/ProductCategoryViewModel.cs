using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.ViewModel.ProductCategoryViewModels
{
    public class ProductCategoryViewModel
    {
        public string Id { get; set; }

        [Required]
        public string Caption { get; set; }
    }
}
