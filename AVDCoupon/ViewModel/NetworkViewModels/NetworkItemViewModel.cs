using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.ViewModel.NetworkViewModels
{
    public class NetworkItemViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Caption { get; set; }

        public byte[] LogoImageView { get; set; }

        public IFormFile LogoImage { get; set; }

    }
}
