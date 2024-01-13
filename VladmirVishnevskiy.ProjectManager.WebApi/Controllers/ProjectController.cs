using Microsoft.AspNetCore.Mvc;
using VladimirVishnevskiy.ProjectManager.Services.Interfaces;
using VladimirVishnevskiy.ProjectManager.Services.Models;
using VladmirVishnevskiy.ProjectManager.WebApi.Models.Project;

namespace VladmirVishnevskiy.ProjectManager.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public IActionResult CreateProject([FromBody] ProjectDto projectRequest)
        {
            var projectDto = new ProjectDto
            {
                Name = projectRequest.Name,
                PerformerName = projectRequest.PerformerName,
                CustomerName = projectRequest.CustomerName,
                ProjectStart = projectRequest.ProjectStart,
                ProjectEnd = projectRequest.ProjectEnd,
                Priority = projectRequest.Priority,
                ProjectManagerId = projectRequest.ProjectManagerId
            };

            _projectService.AddProject(projectDto);

            return Ok(projectDto);
        }

            [HttpGet]
            public IActionResult GetAllProjects()
            {
                var projects = _projectService.GetAllProjects();
                return Ok(projects);
            }

            [HttpGet("{projectId}")]
            public IActionResult GetProjectById(int projectId)
            {
                var project = _projectService.GetProject(projectId);

                if (project == null)
                {
                    return NotFound($"Project with ID {projectId} not found");
                }

                return Ok(project);
            }

            [HttpPut("{projectId}")]
            public IActionResult UpdateProject(int projectId, [FromBody] ProjectDto projectUpdateRequest)
            {
                var existingProject = _projectService.GetProject(projectId);

                if (existingProject == null)
                {
                    return NotFound($"Project Not Found ID {projectId}");
                }
                existingProject.Name = projectUpdateRequest.Name;
                existingProject.PerformerName = projectUpdateRequest.PerformerName;
                existingProject.CustomerName = projectUpdateRequest.CustomerName;
                existingProject.ProjectStart = projectUpdateRequest.ProjectStart;
                existingProject.ProjectEnd = projectUpdateRequest.ProjectEnd;
                existingProject.Priority = projectUpdateRequest.Priority;
                existingProject.ProjectManagerId = projectUpdateRequest.ProjectManagerId;

                _projectService.UpdateProject(existingProject);

                return Ok(existingProject);
            }

            [HttpDelete("{projectId}")]
            public IActionResult DeleteProject(int projectId)
            {
                var existingProject = _projectService.GetProject(projectId);

                if (existingProject == null)
                {
                    return NotFound($"Project Not Found ID {projectId}");
                }

                _projectService.RemoveProject(projectId);

                return NoContent();
            }
        [HttpGet("GetAllSortedProjects")]
        public IActionResult GetAllSortedProjects(
        [FromQuery] DateTime? startDateRange,
        [FromQuery] int? priority,
        [FromQuery] string sortBy)
        {
            var projects = _projectService.GetFilteredAndSortedProjects(startDateRange, priority, sortBy);

            if (projects == null)
            {
                return NotFound("No projects found.");
            }

            return Ok(projects);
        }
    }

}
