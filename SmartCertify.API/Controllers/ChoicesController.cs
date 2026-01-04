using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCertify.Application.DTOs;
using SmartCertify.Application.Interfaces.QuestionChoice;

namespace SmartCertify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChoicesController : ControllerBase
    {
        private readonly IChoiceService _service;

        public ChoicesController(IChoiceService service)
        {
            _service = service;
        }
        [HttpGet("{questionId}")]
        [ProducesResponseType(typeof(IEnumerable<ChoiceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ChoiceDto>>> GetChoices(int questionId)
        {
            var choices = await _service.GetAllChoicesAsync(questionId);
            return Ok(choices);
        }

        [HttpGet("{questionId}/{id}")]
        [ProducesResponseType(typeof(ChoiceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ChoiceDto>> GetChoice(int questionId, int id)
        {
            var choice = await _service.GetChoiceByIdAsync(id);
            if (choice == null)
            {
                return NotFound();
            }
            return Ok(choice);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateChoiceDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateChoice([FromBody] CreateChoiceDto dto)
        {
            await _service.AddChoiceAsync(dto);
            return CreatedAtAction(nameof(GetChoice), new { id = dto.QuestionId }, dto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateChoice(int id, [FromBody] UpdateChoiceDto dto)
        {
            await _service.UpdateChoiceAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> UpdateUserChoice(int id, [FromBody] UpdateUserChoice dto)
        {
            await _service.UpdateUserChoiceAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            await _service.DeleteChoiceAsync(id);
            return NoContent();
        }


        [HttpPost("bulk")]
        [ProducesResponseType(typeof(IEnumerable<ChoiceDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BulkCreateChoices([FromBody] BulkCreateChoiceDto dto)
        {
            // Optional: Check if the question exists (implement this in your service/repository)
            // var questionExists = await _questionService.QuestionExistsAsync(dto.QuestionId);
            // if (!questionExists)
            //     return NotFound($"QuestionId {dto.QuestionId} not found.");

            var createdChoices = new List<ChoiceDto>();
            foreach (var choice in dto.Choices)
            {
                choice.QuestionId = dto.QuestionId; // Ensure all choices are linked to the same question
                await _service.AddChoiceAsync(choice);
                // Optionally, fetch the created choice and add to createdChoices
                // var created = await _service.GetChoiceByIdAsync(choice.ChoiceId);
                // if (created != null) createdChoices.Add(created);
            }

            return CreatedAtAction(nameof(GetChoices), new { questionId = dto.QuestionId }, null);
        }





    }
}
