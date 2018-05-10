using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.ViewModel.NetworkPointViewModels
{
    public class NetworkPointViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Долгота")]
        public string Longitude { get; set; }
        [Display(Name = "Широта")]
        public string Latitude { get; set; }
   
        [Display(Name="Регион")]
        [Required]
        public string Region { get; set; }

        [Display(Name = "Страна")]
        [Required]
        public string Country { get; set; }
        [Display(Name = "Город")]
        [Required]
        public string City { get; set; }
       
        [Display(Name="Адресс")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Сеть")]
        [Required]
        public Guid NetworkId { get; set; }

        [Display(Name = "Сеть")]
        public SelectList Networks { get; set; }

        [Display(Name = "Сеть")]
        public string NetworkName { get; set; }

    }
}
