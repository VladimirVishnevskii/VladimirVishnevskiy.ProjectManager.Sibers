using VladimirVishnevskiy.ProjectManager.Services.Models;

namespace VladimirVishnevskiy.ProjectManager.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDto? GetEmployee (int id);
        IEnumerable<EmployeeDto> GetAllEmployees();
        void AddEmployee(EmployeeDto employee);
        void RemoveEmployee(int id);
        void UpdateEmployee(EmployeeDto employee);
    }
}
