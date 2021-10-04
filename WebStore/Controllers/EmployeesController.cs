using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesController> _Logger;

        public EmployeesController(IEmployeesData EmployeesData, ILogger<EmployeesController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }
        public IActionResult Index()
        {
            return View(_EmployeesData.GetAll());
        }
        public IActionResult Details(int id)
        {
            var employee = _EmployeesData.GetById(id);

            if (employee is null)
                return NotFound(); 
            return View(employee);
        }
        public IActionResult Create() => View("Edit", new EmployeeViewModel());

        public IActionResult Edit (int? id)
        {
            if (id is null)
                return View(new EmployeeViewModel());
            var employee = _EmployeesData.GetById((int)id);
            if (employee is null)
                return NotFound();
            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var employee = new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Patronymic = model.Patronymic,
                Age = model.Age,
            };
            if (employee.Id == 0)
                _EmployeesData.Add(employee); 
            else
                _EmployeesData.Update(employee);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete (int id)
        {
            if (id < 0) return BadRequest();
            var employee = _EmployeesData.GetById(id);
            if (employee is null)
                return NotFound();
            return View(new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Patronymic = employee.Patronymic,
                Age = employee.Age,
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _EmployeesData.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
