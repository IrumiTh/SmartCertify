using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.Questions;

namespace SmartCertify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _service;
        public QuestionsController(IQuestionService servive)
        {
            _service = servive;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QuestionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAllQuestions()
        {
            var questions = await _service.GetAllQuestionsAsync();
            return Ok(questions);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(QuestionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<QuestionDto>> GetQuestion(int id)
        {
            var question = await _service.GetQuestionByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateQuestionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionDto dto)
        {
            await _service.AddQuestionAsync(dto);
            return CreatedAtAction(nameof(GetQuestion), new { id = dto.CourseId }, dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] UpdateQuestionDto dto)
        {
            await _service.UpdateQuestionAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            await _service.DeleteQuestionAsync(id);
            return NoContent();
        }





    }
}
