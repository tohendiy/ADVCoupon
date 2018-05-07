using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ADVCoupon.ViewModel.MailViewModels
{
    public class MailViewModel
    {
        [Display(Name ="Тема")]
        [Required]
        public string Subject { get; set; }

        [Display(Name = "Сообщение")]
        [Required]
        public string Message { get; set; }

        [Display(Name = "Получатели")]
        public MultiSelectList RecipientsRoles { get; set; }

        [Display(Name = "Получатели")]
        [Required]
        public IEnumerable<string> Roles { get; set; }
    }
}
