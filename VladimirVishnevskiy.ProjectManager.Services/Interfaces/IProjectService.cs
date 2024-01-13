using VladimirVishnevskiy.ProjectManager.Services.Models;

namespace VladimirVishnevskiy.ProjectManager.Services.Interfaces
{
    public interface IProjectService
    {
        ProjectDto? GetProject(int id);
        IEnumerable<ProjectDto> GetAllProjects();
        void AddProject(ProjectDto project);
        void RemoveProject(int id);
        void UpdateProject(ProjectDto project);
        IEnumerable<ProjectDto> GetFilteredAndSortedProjects(DateTime? startDateRange, int? priority, string sortBy);
    }
}