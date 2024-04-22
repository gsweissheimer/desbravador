using v1.DTO;
using v1.Entities;
using v1.Enums;

namespace v1.BLL
{
    public interface IProjectUserRecordBLL
    {
        Task<List<ProjectDTO>> GetAllProjects();
        Task<List<ProjectDTO>> GetAllProjectsNoMap();
        Task<ProjectDTO> GetProjectById(int? id);
        Task<ProjectDTO> InsertProject(ProjectDTO project);
        Task<ProjectDTO> UpdateProject(ProjectDTO project);
        Task<bool> DeleteProjectById(int? id);
    }
    public class ProjectUserRecordBLL : IProjectUserRecordBLL
    {
        private readonly IProjectBLL _projectBLL;
        private readonly IUserRecordBLL _userRecordBLL;
        private readonly IProjectUserRecordDAO _projectUserRecordDAO;
        public ProjectUserRecordBLL
        (
            IProjectBLL projectBLL,
            IUserRecordBLL userRecordBLL,
            IProjectUserRecordDAO projectUserRecordDAO
        )
        {
            _projectBLL = projectBLL;
            _userRecordBLL = userRecordBLL;
            _projectUserRecordDAO = projectUserRecordDAO;
        }
        public async Task<List<ProjectDTO>> GetAllProjects()
        {
            List<ProjectDTO> result = new List<ProjectDTO>();

            List<Project> projects = await _projectBLL.GetAllProjects();

            foreach (var project in projects)
            {
                ProjectDTO auxProject = new ProjectDTO();
                UserRecord responsable = await _userRecordBLL.GetUserRecordById(project.ResponmsableId);
                List<UserRecord> users = await _userRecordBLL.GetUsersByProjectId(project.Id);
                auxProject.MapFromObjects(project, responsable, users);
                result.Add(auxProject);
            }
            
            return result;
        }
        public async Task<List<ProjectDTO>> GetAllProjectsNoMap()
        {
            List<ProjectDTO> result = new List<ProjectDTO>();

            List<Project> projects = await _projectBLL.GetAllProjectsNoMap();

            foreach (var project in projects)
            {
                ProjectDTO auxProject = new ProjectDTO();
                UserRecord responsable = await _userRecordBLL.GetUserRecordById(project.ResponmsableId);
                List<UserRecord> users = await _userRecordBLL.GetUsersByProjectId(project.Id);
                auxProject.MapFromObjects(project, responsable, users);
                result.Add(auxProject);
            }
            
            return result;
        }
        public async Task<ProjectDTO> GetProjectById(int? id)
        {
            Project project = await _projectBLL.GetProjectById(id);
            UserRecord responsable = await _userRecordBLL.GetUserRecordById(project.ResponmsableId);
            List<UserRecord> users = await _userRecordBLL.GetUsersByProjectId(project.Id);

            ProjectDTO result = new ProjectDTO();
            result.MapFromObjects(project, responsable, users);
            
            return result;
        }
        public async Task<ProjectDTO> InsertProject(ProjectDTO project)
        {
            Project porjectToInsert = ConvertProjectDTOIntoModel(project);;
            Project insertedProject = await _projectBLL.InsertProject(porjectToInsert);

            await InsertProjectUserRecord(project, insertedProject.Id);
            
            return await GetProjectById(insertedProject.Id);
        }
        public async Task<ProjectDTO> UpdateProject(ProjectDTO project)
        {
            ProjectDTO projectOld = await GetProjectById(project.Id);
            Project porjectToUpdate = ConvertProjectDTOIntoModel(project);
            Project updatedProject = await _projectBLL.UpdateProject(porjectToUpdate);
            SyncProjectUserRecords(projectOld, project);
            ProjectDTO projectToReturn = new ProjectDTO();
            projectToReturn.MapFromObjects(updatedProject, project.Responmsable, project.Users);

            return projectToReturn;

        }
        public async Task<bool> DeleteProjectById(int? id)
        {
            bool projectDeleted = false;
            Project project = await _projectBLL.GetProjectById(id);
            if (CanDeleteProject(project))
            {
                projectDeleted = _projectBLL.DeleteProjectById(id);
            }
            return projectDeleted;
        }
        private bool CanDeleteProject(Project project)
        {
            if (project.Status == ProjectStatus.Completed || project.Status == ProjectStatus.InProgress  || project.Status == ProjectStatus.Started)
                return false;

            return true;
        }
        private async Task InsertProjectUserRecord(ProjectDTO project, int projectId)
        {
            foreach (UserRecord user in project.Users)
            {
                ProjectUserRecord projectUserRecordToInsert = new ProjectUserRecord{
                    ProjectId = projectId,
                    UserRecordId = user.Id
                };
                await _projectUserRecordDAO.InsertProjectUserRecord(projectUserRecordToInsert);
            }
        }
        private Project ConvertProjectDTOIntoModel(ProjectDTO project)
        {
            return new Project{
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                Status = project.Status,
                Risk = project.Risk,
                ResponmsableId = project.Responmsable.Id
            };
        }
        private void SyncProjectUserRecords(ProjectDTO projectOld, ProjectDTO projectNew)
        {
            List<ProjectUserRecord> oldProjectUserRecord = MakeUsersProjectUsers(projectOld.Users, projectOld.Id);
            List<ProjectUserRecord> newProjectUserRecord = MakeUsersProjectUsers(projectNew.Users, projectNew.Id);

            foreach (var oldRecord in oldProjectUserRecord)
            {
                if (!newProjectUserRecord.Any(newRecord => newRecord.ProjectId == oldRecord.ProjectId && newRecord.UserRecordId == oldRecord.UserRecordId))
                {
                    _projectUserRecordDAO.DeleteProjectUserRecord(oldRecord);
                }
            }
            foreach (var newRecord in newProjectUserRecord)
            {
                if (!oldProjectUserRecord.Any(oldRecord => oldRecord.ProjectId == newRecord.ProjectId && oldRecord.UserRecordId == newRecord.UserRecordId))
                {
                    _projectUserRecordDAO.InsertProjectUserRecord(newRecord);
                }
            }
        }
        private List<ProjectUserRecord> MakeUsersProjectUsers(List<UserRecord> users, int projectId)
        {
            var projectUserRecords = new List<ProjectUserRecord>();

            foreach (var user in users)
            {
                var projectUserRecord = new ProjectUserRecord
                {
                    ProjectId = projectId,
                    UserRecordId = user.Id,
                };

                projectUserRecords.Add(projectUserRecord);
            }

            return projectUserRecords;
        }

    }
}
