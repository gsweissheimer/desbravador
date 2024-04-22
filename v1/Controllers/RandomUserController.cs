using Microsoft.AspNetCore.Mvc;
using v1.BLL;
using v1.DTO;
using v1.Entities;

namespace v1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomUserController : ControllerBase
    {

        private readonly IRandomUserBLL _randomUserBLL;

        public RandomUserController
        (
            IRandomUserBLL randomUserBLL
        )
        {
            _randomUserBLL = randomUserBLL;
        }

        [HttpGet]
        public async Task<RandomUserDTO> GetRandomUser()
        {
            return await _randomUserBLL.GetRandomUser();
        }
    }
}
