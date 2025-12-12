using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCertify.Application.Interfaces.Courses;

namespace SmartCertify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;

        public CourseController(ICourseService service)
        {
            this._service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCoursesAsync()
        {
            var courses = await _service.GetAllCoursesAsync();
            return Ok(courses);
        }
    }
}
