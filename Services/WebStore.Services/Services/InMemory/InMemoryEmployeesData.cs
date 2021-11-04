using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;
using WebStore.Services.Data;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> logger;

        private int _CurrentMaxId;

        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> Logger)
        {
            logger = Logger;
            _CurrentMaxId = TestData.Employees.Max(e => e.Id);
        }
        public int Add(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            if (TestData.Employees.Contains(employee)) return employee.Id;

            employee.Id = _CurrentMaxId++;
            TestData.Employees.Add(employee);

            logger.LogInformation("Сотрудник {0} успешно добавлен", employee);

            return employee.Id;
        }

        public bool Delete(int id)
        {
            var db_employee = GetById(id);
            if (db_employee is null)
            {
                logger.LogInformation("В процессе попытки удаления сотрудник с id:{0} не найден", id);
                return false;
            }

            TestData.Employees.Remove(db_employee);

            logger.LogInformation("Сотрудник {0} успешно удален", db_employee);

            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            return TestData.Employees;
        }

        public Employee GetById(int id)
        {
            return TestData.Employees.SingleOrDefault(e => e.Id == id);
        }

        public void Update(Employee employee)
        {
            if (employee is null) throw new ArgumentNullException(nameof(employee));
            if (TestData.Employees.Contains(employee)) return;
            var db_employee = GetById(employee.Id);
            if (db_employee is null)
            {
                logger.LogInformation("В процессе попытки удаления сотрудник с id:{0} не найден", db_employee.Id);
                return;
            }

            db_employee.LastName = employee.LastName;
            db_employee.FirstName = employee.FirstName;
            db_employee.Patronymic = employee.Patronymic;
            db_employee.Age = employee.Age;

            logger.LogInformation("Сотрудник {0} успешно обновлен", employee);

        }
    }
}
