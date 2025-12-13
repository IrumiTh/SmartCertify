using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.Courses;

namespace SmartCertify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        private readonly IValidator<CreateCourseDto> _validator;
        private readonly IValidator<UpdateCourseDto> _updateValidator;

        public CourseController(ICourseService service, IValidator<CreateCourseDto> validator, IValidator<UpdateCourseDto> updateValidator)
        {
            this._validator = validator;
            this._service = service;
            this._updateValidator = updateValidator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            return Ok(await _service.GetAllCoursesAsync());
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<CourseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _service.GetCourseByIdAsync(id);
            return course == null ? NotFound() : Ok(course);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateCourseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto createCourseDto)
        {
            var validationResult = await _validator.ValidateAsync(createCourseDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            await _service.AddCourseAsync(createCourseDto);
            return CreatedAtAction(nameof(GetCourse), new { id = createCourseDto.Title }, createCourseDto);
        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] UpdateCourseDto updateCourseDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(updateCourseDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            await _service.UpdateCourseAsync(id, updateCourseDto);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _service.DeleteCourseAsync(id);
            return NoContent();
        }


        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateDescription([FromRoute] int id, [FromBody] CourseUploadDescriptionDto model)
        {
            await _service.UpdateDescriptionAsync(id, model.Description);
            return NoContent();
        }

    }
}
