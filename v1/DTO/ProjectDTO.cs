using v1.Entities;

namespace v1.DTO
{
    public class ProjectDTO
    {
        public int Id  { get; set; }
        public string Name  { get; set; }
        public string Description  { get; set; }
        public DateTime StartDate  { get; set; }
        public DateTime? EndDate  { get; set; }
        public string Status  { get; set; }
        public string Risk  { get; set; }
        public UserRecord Responmsable  { get; set; }
        public List<UserRecord>? Users  { get; set; }
        public DateTime CreatedAt  { get; set; }
        public DateTime? UpdatedAt  { get; set; }
        public bool IsArchived { get; set; }
        public void MapFromObjects(Project project, UserRecord responsable, List<UserRecord> users)
        {
            Id = project.Id;
            Name = project.Name;
            Description = StringService.LimitByWords(project.Description, 15);
            StartDate = project.StartDate;
            EndDate = project.EndDate;
            Status = project.Status;
            Risk = project.Risk;
            Responmsable = responsable; 
            Users = users;
            CreatedAt = project.CreatedAt;
            UpdatedAt = project.UpdatedAt;
            IsArchived = project.IsArchived;
        }
    }
}
