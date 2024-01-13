using System.ComponentModel.DataAnnotations.Schema;
using VladimirVishnevskiy.ProjectManager.Data.Entities.Base;

namespace VladimirVishnevskiy.ProjectManager.Data.Entities
{
    public class ProjectEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string PerformerName { get; set; } = string.Empty;
        public DateTime ProjectStart { get; set; }
        public DateTime? ProjectEnd { get; set; }
        public int Priority { get; set; }
        public EmployeeEntity ProjectManager { get; set; } = null!;        
        public virtual IEnumerable<EmployeeEntity> Employees { get; set; } = new List<EmployeeEntity>();
    }
}
