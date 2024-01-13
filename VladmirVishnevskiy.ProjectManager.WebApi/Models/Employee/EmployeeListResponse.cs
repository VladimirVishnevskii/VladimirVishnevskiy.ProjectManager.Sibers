namespace VladmirVishnevskiy.ProjectManager.WebApi.Models.Employee
{
    public class EmployeeListResponse
    {
        public IEnumerable<EmployeeResponse> Employees { get; set; } = new List<EmployeeResponse>();
        public int Count => Employees.Count();
    }
}
