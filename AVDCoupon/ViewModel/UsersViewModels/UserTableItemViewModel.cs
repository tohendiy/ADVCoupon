using AVDCoupon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.UsersViewModels
{
    public class UserTableItemViewModel
    {
        [Required]
        [Display(Name = "Роль")]
        public string Role { get; set; }
        [Display(Name = "Пользователь")]
        public ApplicationUser User { get; set; }

        [Display(Name = "Сеть")]
        public string NetworkName { get; set; }
        [Display(Name = "Поставщик")]
        public string ProviderName { get; set; }
    }
}
