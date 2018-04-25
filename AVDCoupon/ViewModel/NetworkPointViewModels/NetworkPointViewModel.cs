using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.ViewModel.NetworkPointViewModels
{
    public class NetworkPointViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Accuracy { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }

        public Guid NetworkId { get; set; }
        public SelectList Networks { get; set; }

    }
}
