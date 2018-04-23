using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.ViewModel.ProductCategoryViewModels
{
    public class ProductCategoryViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Caption { get; set; }
    }
}
