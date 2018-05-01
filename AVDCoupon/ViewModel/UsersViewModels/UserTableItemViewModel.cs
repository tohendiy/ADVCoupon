using AVDCoupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.UsersViewModels
{
    public class UserTableItemViewModel
    {
        public string Role { get; set; }
        public ApplicationUser User { get; set; }
        public string NetworkName { get; set; }
        public string ProviderName { get; set; }
    }
}
