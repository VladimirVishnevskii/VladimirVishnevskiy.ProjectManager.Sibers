namespace VladmirVishnevskiy.ProjectManager.WebApi.Models.Project
{
    public class ProjectCreateRequest
    {
        public string Name { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string PerformerName { get; set; } = string.Empty;
        public DateTime ProjectStart { get; set; }
        public DateTime? ProjectEnd { get; set; }
        public int Priority { get; set; }
        public int ProjectManagerId { get; set; }
    }
}
