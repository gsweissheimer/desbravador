using System.Data;
using v1.DTO;
using v1.Entities;
using v1.Repository;

namespace v1.BLL
{
    public interface IUserRecordDAO
    {
        Task<List<UserRecord>> GetAllUsersRecords();
        Task<UserRecord> GetUserRecordById(int id);
        Task<List<UserRecord>> GetUsersByProjectId(int id);
        Task<UserRecord> InsertUserRecord(UserRecord userRecord);
    }
    public class UserRecordDAO : IUserRecordDAO
    {
        private readonly IRepository _repository;
        public UserRecordDAO
        (
            IRepository repository
        )
        {
            _repository = repository;
        }
        public async Task<List<UserRecord>> GetAllUsersRecords()
        {
            List<UserRecord> returnedUsers = await _repository.ExecuteQuery
            (@$"
                select
                    *
                    from user_record;
            ", UserRecordMapper.MapUserRecord);
            return returnedUsers;
        }
        public async Task<UserRecord> GetUserRecordById(int id)
        {
            List<UserRecord> returnedUser = await _repository.ExecuteQuery
            (@$"
                select
                    *
                    from user_record
                    where id = {id};
            ", UserRecordMapper.MapUserRecord);
            return returnedUser.SingleOrDefault();
        }
        public async Task<List<UserRecord>> GetUsersByProjectId(int id)
        {
            List<UserRecord> returnedUsers = await _repository.ExecuteQuery
            (@$"
                select
                    *
                    from user_record ur
                    join project_user_record pur
                        on ur.id = pur.user_record_id
                    where pur.project_id = {id};  
            ", UserRecordMapper.MapUserRecord);
            return returnedUsers;
        }
        public async Task<UserRecord> InsertUserRecord(UserRecord userRecord)
        {
            List<UserRecord> returnedUser = await _repository.ExecuteQuery
            (@$"
                INSERT INTO user_record(
                    first_name,
                    last_name,
                    email
                ) values (
                    '{userRecord.FirstName}',
                    '{userRecord.LastName}',
                    '{userRecord.Email}'
                ) RETURNING *;
            ", UserRecordMapper.MapUserRecord);
            return returnedUser.SingleOrDefault();
        }
    }
}
