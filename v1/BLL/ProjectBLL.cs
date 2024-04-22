using System;
using v1.Entities;

namespace v1.BLL
{
    public interface IProjectBLL
    {
        Task<List<Project>> GetAllProjects();
        Task<List<Project>> GetAllProjectsNoMap();
        Task<Project> GetProjectById(int? id);
        Task<Project> InsertProject(Project project);
        Task<Project> UpdateProject(Project project);
        bool DeleteProjectById(int? id);
    }
    public class ProjectBLL : IProjectBLL
    {
        private readonly IProjectDAO _projectDAO;
        public ProjectBLL
        (
            IProjectDAO projectDAO
        )
        {
            _projectDAO = projectDAO;
        }
        public async Task<List<Project>> GetAllProjects()
        {       
            return await _projectDAO.GetAllProjects();
        }
        public async Task<List<Project>> GetAllProjectsNoMap()
        {       
            return await _projectDAO.GetAllProjectsNoMap();
        }
        public async Task<Project> GetProjectById(int? id)
        {       
            return await _projectDAO.GetProjectById(id);
        }
        public async Task<Project> InsertProject(Project project)
        {
            return await _projectDAO.InsertProject(project);
        }
        public async Task<Project> UpdateProject(Project project)
        {
            if (project.EndDate != null) _projectDAO.SetProjectEndDate((DateTime) project.EndDate, project.Id);
            return await _projectDAO.UpdateProject(project);
        }
        public bool DeleteProjectById(int? id)
        {
            return _projectDAO.DeleteProjectById(id);
        }
    }
}
