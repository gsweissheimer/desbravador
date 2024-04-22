using Microsoft.AspNetCore.Mvc;
using v1.BLL;
using v1.DTO;

namespace v1.Controllers
{
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectUserRecordBLL _projectUserRecordBLL;

        public ProjectController
        (
            IProjectUserRecordBLL projectUserRecordBLL
        )
        {
            _projectUserRecordBLL = projectUserRecordBLL;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAllProjects()
        {
            List<ProjectDTO> projetcs = await _projectUserRecordBLL.GetAllProjectsNoMap();

            return Ok(projetcs);
        }

        [HttpGet]
        [Route("/api/[controller]/{id:int}")]
        public async Task<IActionResult> GetProjectById(int? id)
        {
            ProjectDTO projetc = await _projectUserRecordBLL.GetProjectById(id);

            return Ok(projetc);
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> InsertProject([FromBody] ProjectDTO project)
        {
            ProjectDTO projetc = await _projectUserRecordBLL.InsertProject(project);

            return Ok(projetc);
        }

        [HttpPatch]
        [Route("api/[controller]")]
        public  async Task<IActionResult> UpdateProject([FromBody] ProjectDTO project)
        {
            ProjectDTO projetc = await _projectUserRecordBLL.UpdateProject(project);

            return Ok(projetc);
        }

        [HttpDelete]
        [Route("/api/[controller]/{id:int}")]
        public  async Task<IActionResult> DeleteProjectById(int? id)
        {
            bool deleted = await _projectUserRecordBLL.DeleteProjectById(id);
            return Ok(deleted);
        }
    }
}
