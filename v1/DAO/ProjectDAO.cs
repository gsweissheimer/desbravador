using System;
using System.Data;
using v1.DTO;
using v1.Entities;
using v1.Repository;

namespace v1.BLL
{
    public interface IProjectDAO
    {
        Task<List<Project>> GetAllProjectsNoMap();
        Task<List<Project>> GetAllProjects();
        Task<Project> GetProjectById(int? id);
        Task<Project> InsertProject(Project project);
        Task<Project> UpdateProject(Project project);
        void SetProjectEndDate(DateTime endDate, int projectI);
        bool DeleteProjectById(int? id);
    }
    public class ProjectDAO : IProjectDAO
    {
        private readonly IRepository _repository;
        public ProjectDAO
        (
            IRepository repository
        )
        {
            _repository = repository;
        }
        public async Task<List<Project>> GetAllProjectsNoMap()
        {
            List<Project> returnedProjects = await _repository.ExecuteQueryNoMap<Project>
            (@$"
                select
                    *
                    from project
                    where is_archived is false;
            ");
            return returnedProjects;
        }
        public async Task<List<Project>> GetAllProjects()
        {
            List<Project> returnedProjects = await _repository.ExecuteQuery
            (@$"
                select
                    *
                    from project
                    where is_archived is false;
            ",ProjectMapper.MapProject);
            return returnedProjects;
        }
        public async Task<Project> GetProjectById(int? id)
        {
            List<Project> returnedProject = await _repository.ExecuteQuery
            (@$"
                select
                    *
                    from project
                    where id = {id};
            ", ProjectMapper.MapProject);
            return returnedProject.SingleOrDefault();
        }
        public async Task<Project> InsertProject(Project project)
        {
            List<Project> returnedProject = await _repository.ExecuteQuery
            (@$"
                INSERT INTO project(
                    name,
                    description,
                    start_date,
                    responsible_id
                ) values (
                    '{project.Name}',
                    '{project.Description}',
                    '{project.StartDate}',
                    {project.ResponmsableId}
                ) RETURNING *;
            ", ProjectMapper.MapProject);
            return returnedProject.SingleOrDefault();
        }
        public async Task<Project> UpdateProject(Project project)
        {
            List<Project> returnedProject = await _repository.ExecuteQuery
            (@$"
                UPDATE project
                    SET
                        name = '{project.Name}',
                        description = '{project.Description}',
                        start_date = '{project.StartDate}',
                        status = '{project.Status}',
                        risk = '{project.Risk}',
                        responsible_id = {project.ResponmsableId}
                    WHERE
                        id = {project.Id}
                    RETURNING *;
            ", ProjectMapper.MapProject);
            return returnedProject.SingleOrDefault();
        }
        public void SetProjectEndDate(DateTime endDate, int projectId)
        {
            _repository.ExecuteQuery
            (@$"
                update project
                    set end_date = {endDate}
                    where id = {projectId};
            ");
        }
        public bool DeleteProjectById(int? id)
        {
            bool deleted = _repository.ExecuteQuery
            (@$"
                update project
                    set is_archived = true
                    where id = {id};
            ");
            return deleted;
        }
    }
}
