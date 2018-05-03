using System;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.ViewModel.ProductCategoryViewModels
{
    public class ProductCategoryViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Caption { get; set; }
    }
}
