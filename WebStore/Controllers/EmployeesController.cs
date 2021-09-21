using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> _Employees = new()
        {
            new Employee { Id = 1, FirstName = "Иван", LastName = "Самоедов", Patronymic = "Олегович", Age = 33 },
            new Employee { Id = 2, FirstName = "Гоша", LastName = "Патрив", Patronymic = "Иванович", Age = 23 },
            new Employee { Id = 3, FirstName = "Роман", LastName = "Ростков", Patronymic = "Витальевич", Age = 35 },
            new Employee { Id = 4, FirstName = "Мария", LastName = "Мирошкина", Patronymic = "Григорьевна", Age = 29 },
            new Employee { Id = 5, FirstName = "Алексей", LastName = "Морозов", Patronymic = "Владимирович", Age = 63 },
        };
        public IActionResult Index()
        {
            return View(_Employees);
        }
    }
}
