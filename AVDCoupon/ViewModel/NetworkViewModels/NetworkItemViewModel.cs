using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.ViewModel.NetworkViewModels
{
    public class NetworkItemViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Caption { get; set; }

        [Display(Name = "Логотип")]
        public byte[] LogoImageView { get; set; }

        [Display(Name = "Логотип")]
        [DataType(DataType.Upload)]
        public IFormFile LogoImage { get; set; }

        public Guid ProductCategoryId { get; set; }

        public SelectList ProductCategories { get; set; }
    }
}
