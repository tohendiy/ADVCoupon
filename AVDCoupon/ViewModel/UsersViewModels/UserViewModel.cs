using ADVCoupon.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ADVCoupon.ViewModel.UsersViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Поставщик")]
        public SelectList Providers { get; set; }
        [Display(Name = "Сеть")]
        public SelectList Networks { get; set; }

        [Display(Name="Имя")]
        public string Name { get; set; }

        [Display(Name = "Роль")]
        [Required]
        public string Role { get; set; }

        [Display(Name = "Почтовый адрес")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Подтвердить пароль")]
        //[Required]
        //[Compare("Password", ErrorMessage = "Пароли не совпадают")]
        //public string ConfrimPassword { get; set; }

        [Display(Name = "Поставщик")]
        public string Provider { get; set; }
        [Display(Name = "Сеть")]
        public string Network { get; set; }
    }
}
