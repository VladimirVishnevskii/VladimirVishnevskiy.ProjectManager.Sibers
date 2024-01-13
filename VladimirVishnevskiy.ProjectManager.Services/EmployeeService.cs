using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VladimirVishnevskiy.ProjectManager.Data;
using VladimirVishnevskiy.ProjectManager.Data.Entities;
using VladimirVishnevskiy.ProjectManager.Services.Interfaces;
using VladimirVishnevskiy.ProjectManager.Services.Models;

namespace VladimirVishnevskiy.ProjectManager.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEmployee(EmployeeDto employeeDto)
        {
            var employee = new EmployeeEntity
            {

                Name = employeeDto.Name,
                Surname = employeeDto.Surname,
                Patronymic = employeeDto.Patronymic,
                Email = employeeDto.Email
            };
            _dbContext.Employees.Add(employee);
            _dbContext.SaveChanges();
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            var employees = _dbContext.Employees
                .Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Patronymic = x.Patronymic,
                    Email = x.Email
                })
                .ToList();

            return employees;
        }

        public EmployeeDto? GetEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee == null)
            {
                return null;
            }

            var employeeDto = new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Surname = employee.Surname,
                Patronymic = employee.Patronymic,
                Email = employee.Email
            };

            return employeeDto;
        }

        public void RemoveEmployee(int id)
        {
            var employee = _dbContext.Employees.Find(id);

            if (employee != null)
            {
                _dbContext.Employees.Remove(employee);
                _dbContext.SaveChanges();
            }
        }

        public void UpdateEmployee(EmployeeDto employeeDto)
        {
            var employee = _dbContext.Employees.Find(employeeDto.Id);

            if (employee != null)
            {
                employee.Name = employeeDto.Name;
                employee.Surname = employeeDto.Surname;
                employee.Patronymic = employeeDto.Patronymic;
                employee.Email = employeeDto.Email;

                _dbContext.SaveChanges();
            }
        }
    }
}

