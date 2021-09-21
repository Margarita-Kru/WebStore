using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        public readonly IEnumerable<Employee> _Employees;

        public EmployeesController()
        {
            _Employees = TestData.Employees;
        }
        public IActionResult Index()
        {
            return View(_Employees);
        }
    }
}
