namespace VladmirVishnevskiy.ProjectManager.WebApi.Models.Employee
{
    public class EmployeeFullResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Patronymic { get; set; }
        public string Email { get; set; } = null!;
    }
}
