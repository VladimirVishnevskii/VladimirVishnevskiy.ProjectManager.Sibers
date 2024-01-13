using VladimirVishnevskiy.ProjectManager.Data;
using VladimirVishnevskiy.ProjectManager.Data.Entities;
using VladimirVishnevskiy.ProjectManager.Services.Interfaces;
using VladimirVishnevskiy.ProjectManager.Services.Models;

namespace VladimirVishnevskiy.ProjectManager.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddProject(ProjectDto project)
        {
            var projectManagerId = project.ProjectManagerId;

            var employee = _context.Employees.Find(projectManagerId);

            if (employee == null)
            {
                return;
            }

            var projectEntity = new ProjectEntity
            {
                Name = project.Name,
                CustomerName = project.CustomerName,
                PerformerName = project.PerformerName,
                Priority = project.Priority,
                ProjectEnd = project.ProjectEnd,
                ProjectStart = project.ProjectStart,
                ProjectManager = employee
            };

            _context.Projects.Add(projectEntity);
            _context.SaveChanges();
        }

        public IEnumerable<ProjectDto> GetAllProjects()
        {
            return _context.Projects.Select(x => new ProjectDto
            {
                Id = x.Id,
                Name = x.Name,
                CustomerName = x.CustomerName,
                PerformerName = x.PerformerName,
                Priority = x.Priority,
                ProjectEnd = x.ProjectEnd,
                ProjectStart = x.ProjectStart,
                ProjectManagerId = x.ProjectManager.Id
            }).ToList();
        }

        public ProjectDto? GetProject(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return null;
            }

            return new ProjectDto
            {
                Id = project.Id,
                Name = project.Name,
                CustomerName = project.CustomerName,
                PerformerName = project.PerformerName,
                Priority = project.Priority,
                ProjectEnd = project.ProjectEnd,
                ProjectStart = project.ProjectStart,
                ProjectManagerId = project.ProjectManager.Id
            };
        }

        public void RemoveProject(int id)
        {
            var project = _context.Projects.Find(id);

            if (project == null)
            {
                return;
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
        }

        public void UpdateProject(ProjectDto project)
        {
            var projectId = project.Id;
            
            var projectEntity = _context.Projects.Find(projectId);

            if(projectEntity == null)
            {
                return;
            }

            var newManagerId = project.ProjectManagerId;

            var employee = _context.Employees.Find(newManagerId);

            if (employee == null)
            {
                return;
            }

            projectEntity.Name = project.Name;
            projectEntity.Priority = project.Priority;
            projectEntity.ProjectStart = project.ProjectStart;
            projectEntity.ProjectEnd = project.ProjectEnd;
            projectEntity.CustomerName = project.CustomerName;
            projectEntity.PerformerName = project.PerformerName;
            projectEntity.ProjectManager = employee;
        }
        public IEnumerable<ProjectDto> GetFilteredAndSortedProjects(DateTime? startDateRange, int? priority, string sortBy)
        {
            var query = _context.Projects.AsQueryable();

            if (startDateRange.HasValue)
            {
                query = query.Where(p => p.ProjectStart >= startDateRange.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(p => p.Priority == priority.Value);
            }

            query = sortBy switch
            {
                "Name" => query.OrderBy(p => p.Name),
                "CustomerName" => query.OrderBy(p => p.CustomerName),
                "PerformerName" => query.OrderBy(p => p.PerformerName),
                "Priority" => query.OrderBy(p => p.Priority),
                _ => query.OrderBy(p => p.Id),
            };
            var projects = query
                .Select(p => new ProjectDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CustomerName = p.CustomerName,
                    PerformerName = p.PerformerName,
                    Priority = p.Priority,
                    ProjectEnd = p.ProjectEnd,
                    ProjectStart = p.ProjectStart,
                    ProjectManagerId = p.ProjectManager.Id
                })
                .ToList();

            return projects;
        }
    }
}