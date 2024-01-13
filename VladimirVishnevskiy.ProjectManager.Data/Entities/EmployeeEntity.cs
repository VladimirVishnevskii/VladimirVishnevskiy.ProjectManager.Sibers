using VladimirVishnevskiy.ProjectManager.Data.Entities.Base;

namespace VladimirVishnevskiy.ProjectManager.Data.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string? Patronymic { get; set; }
        public string Email { get; set; } = string.Empty;
        public virtual IEnumerable<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
    }
}