using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.ViewModel.ProviderViewModels
{
    public class ProviderItemViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public byte[] LogoImageView { get; set; }

        public IFormFile LogoImage { get; set; }

        public Guid RetailUserId { get; set; }

        public SelectList RetailUsers { get; set; }
    }
}
