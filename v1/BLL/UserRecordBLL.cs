using v1.DTO;
using v1.Entities;

namespace v1.BLL
{
    public interface IUserRecordBLL
    {
        Task<List<UserRecord>> GetAllUsersRecords();
        Task<UserRecord> GetUserRecordById(int id);
        Task<List<UserRecord>> GetUsersByProjectId(int id);
        Task<UserRecord> InsertUserRecord(UserRecord userRecord);
    }
    public class UserRecordBLL : IUserRecordBLL
    {
        private readonly IUserRecordDAO _userRecordDAO;
        public UserRecordBLL
        (
            IUserRecordDAO userRecordDAO
        )
        {
            _userRecordDAO = userRecordDAO;
        }
        public async Task<List<UserRecord>> GetAllUsersRecords()
        {       
            return await _userRecordDAO.GetAllUsersRecords();
        }
        public async Task<UserRecord> GetUserRecordById(int id)
        {
            return await _userRecordDAO.GetUserRecordById(id);
        }
        public async Task<List<UserRecord>> GetUsersByProjectId(int id)
        {
            return await _userRecordDAO.GetUsersByProjectId(id);
        }
        public async Task<UserRecord> InsertUserRecord(UserRecord userRecord)
        {
            return await _userRecordDAO.InsertUserRecord(userRecord);
        }
    }
}
