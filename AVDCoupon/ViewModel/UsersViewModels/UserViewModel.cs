using ADVCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.UsersViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public SelectList Providers { get; set; }
        public SelectList Networks { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfrimPassword { get; set; }
        public string Provider { get; set; }
        public string Network { get; set; }
    }
}
