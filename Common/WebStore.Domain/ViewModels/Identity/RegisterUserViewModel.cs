using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Domain.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name ="Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        [Display(Name = "Подтверждение пароля")]
        public string PasswordConfirm { get; set; }

        
    }
}
