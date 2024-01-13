using Microsoft.AspNetCore.Mvc;
using VladimirVishnevskiy.ProjectManager.Services.Interfaces;
using VladimirVishnevskiy.ProjectManager.Services.Models;
using VladmirVishnevskiy.ProjectManager.WebApi.Models.Employee;

namespace VladmirVishnevskiy.ProjectManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EmployeeController: ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public ActionResult<EmployeeListResponse> GetAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();

            var employeeList = new EmployeeListResponse()
            {
                Employees = employees.Select(x => new EmployeeResponse
                {
                    Id = x.Id,
                    FullName = string.Concat(x.Name, x.Surname, x.Patronymic ?? string.Empty),
                })
            };

            return Ok(employeeList);
        }

        [HttpGet("{employeeId}")]
        public ActionResult<EmployeeFullResponse> GetEmployee(int employeeId)
        {
            var employee = _employeeService.GetEmployee(employeeId);

            if (employee == null)
            {
                return NotFound($"Employee with ID {employeeId} not found");
            }

            var employeeResult = new EmployeeFullResponse
            {
                Id = employeeId,
                Email = employee.Email,
                Name = employee.Name,
                Patronymic = employee.Patronymic,
                Surname = employee.Surname
            };

            return Ok(employeeResult);
        }

        [HttpPost]
        public ActionResult CreateEmployee([FromBody] EmployeeRequest employeeRequest)
        {
            var employeeDto = new EmployeeDto
            {
                Email = employeeRequest.Email,
                Name = employeeRequest.Name,
                Patronymic = employeeRequest.Patronymic,
                Surname = employeeRequest.Surname
            };

            _employeeService.AddEmployee(employeeDto);

            return Ok(employeeRequest);
        }

        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeDto employeeDto)
        {
            var existingEmployee = _employeeService.GetEmployee(employeeId);

            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {employeeId} not found");
            }

            existingEmployee.Name = employeeDto.Name;
            existingEmployee.Surname = employeeDto.Surname;
            existingEmployee.Patronymic = employeeDto.Patronymic;
            existingEmployee.Email = employeeDto.Email;

            _employeeService.UpdateEmployee(existingEmployee);

            return Ok(existingEmployee);
        }

        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(int employeeId)
        {
            var existingEmployee = _employeeService.GetEmployee(employeeId);

            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {employeeId} not found");
            }

            _employeeService.RemoveEmployee(employeeId);

            return Ok();
        }
        
    }
}

