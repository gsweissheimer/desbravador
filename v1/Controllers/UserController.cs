using Microsoft.AspNetCore.Mvc;
using v1.BLL;
using v1.DTO;
using v1.Entities;

namespace v1.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IProjectUserRecordBLL _projectUserRecordBLL;
        private readonly IUserRecordBLL _userRecordBLL;

        public UserController
        (
            IProjectUserRecordBLL projectUserRecordBLL,
            IUserRecordBLL userRecordBLL
        )
        {
            _projectUserRecordBLL = projectUserRecordBLL;
            _userRecordBLL = userRecordBLL;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetUser()
        {
            List<UserRecord> response = await _userRecordBLL.GetAllUsersRecords();
            return Ok(response);
        }

        [HttpGet]
        [Route("/api/[controller]/{id:int}")]
        public async Task<IActionResult> GetUserRecordById(int id)
        {
            UserRecord response = await _userRecordBLL.GetUserRecordById(id);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> InsertUser([FromBody] UserRecord user)
        {
            UserRecord response = await _userRecordBLL.InsertUserRecord(user);
            return Ok(response);
        }
    }
}
