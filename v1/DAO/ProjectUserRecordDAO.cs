using System.Data;
using v1.DTO;
using v1.Entities;
using v1.Repository;

namespace v1.BLL
{
    public interface IProjectUserRecordDAO
    {
        Task<ProjectUserRecord> InsertProjectUserRecord(ProjectUserRecord projectUserRecord);
        void DeleteProjectUserRecord(ProjectUserRecord projectUserRecord);
    }
    public class ProjectUserRecordDAO : IProjectUserRecordDAO
    {
        private readonly IRepository _repository;
        public ProjectUserRecordDAO
        (
            IRepository repository
        )
        {
            _repository = repository;
        }
        public async Task<ProjectUserRecord> InsertProjectUserRecord(ProjectUserRecord projectUserRecord)
        {
            List<ProjectUserRecord> insertedProjectUserRecord = await _repository.ExecuteQuery
            (@$"
                INSERT INTO project_user_record(
                    project_id,
                    user_record_id
                ) values (
                    {projectUserRecord.ProjectId},
                    {projectUserRecord.UserRecordId}
                ) RETURNING *;
            " , ProjectUserRecordMapper.MapProject);
            return insertedProjectUserRecord.SingleOrDefault();
        }  
        public void DeleteProjectUserRecord(ProjectUserRecord projectUserRecord)
        {
            _repository.ExecuteQuery
            (@$"
                DELETE 
                    FROM project_user_record
                    WHERE project_id = {projectUserRecord.ProjectId}
                    AND user_record_id = {projectUserRecord.UserRecordId};
            ");
        } 
    }
}
