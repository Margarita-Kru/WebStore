using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Необходимо указать имя сотрудника!")]
        [Display(Name="Имя")]
        [StringLength(100, MinimumLength =2, ErrorMessage ="Длина имени сотрудника от 2 до 100 символов!")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage ="Введены недопустимые символы!")]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина фамилии сотрудника от 2 до 100 символов!")]
        [Required(ErrorMessage = "Необходимо указать фамилию сотрудника!")]
        [Display(Name = "Фамилия")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Введены недопустимые символы!")]
        public string LastName { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина отчества сотрудника от 2 до 100 символов!")]
        [Display(Name = "Отчество")]
        [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Введены недопустимые символы!")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Необходимо указать возраст сотрудника!")]
        [Display(Name = "Возраст")]
        [Range(18,80, ErrorMessage ="Возраст сотрудника находится в интервале от 18 до 80 лет!")]
        public int Age { get; set; }
    }
}
