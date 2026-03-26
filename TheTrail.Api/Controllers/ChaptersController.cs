using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheTrail.Services.Core.DTOs.Chapter;
using TheTrail.Services.Core.Interfaces;

namespace TheTrail.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChaptersController : ControllerBase
    {
        private readonly IChapterService _chapterService;

        public ChaptersController(IChapterService chapterService)
        {
            _chapterService = chapterService;
        }

        [HttpGet("era/{eraId}")]
        public async Task<ActionResult<IEnumerable<ChapterDto>>> GetByEra(int eraId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            IEnumerable<ChapterDto> chapters = await _chapterService.GetByEraAsync(eraId, userId);

            return Ok(chapters);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ChapterDto>> GetById(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ChapterDto? chapter = await _chapterService.GetByIdAsync(id, userId);

            if (chapter == null)
            {
                return NotFound(new { message = $"Chapter with id {id} was not found." });
            }

            return Ok(chapter);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ChapterDto>> Create([FromBody] CreateChapterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ChapterDto created = await _chapterService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<ChapterDto>> Update(int id, [FromBody] UpdateChapterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ChapterDto? updated = await _chapterService.UpdateAsync(id, dto);

            if (updated == null)
            {
                return NotFound(new { message = $"Chapter with id {id} was not found." });
            }

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int id)
        {
            bool deleted = await _chapterService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new { message = $"Chapter with id {id} was not found." });
            }

            return NoContent();
        }

        [HttpPost("{id}/complete-scroll")]
        [Authorize]
        public async Task<ActionResult<bool>> CompleteScroll(int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized(new { message = "You must be logged in to track progress." });
            }

            bool result = await _chapterService.CompleteScrollAsync(id, userId);

            return Ok(result);
        }

        [HttpGet("{id}/quiz")]
        public async Task<ActionResult<QuizDto>> GetQuiz(int id)
        {
            QuizDto? quiz = await _chapterService.GetQuizAsync(id);
            if (quiz == null)
            {
                return NotFound(new { message = $"No quiz found for chapter {id}." });
            }
            return Ok(quiz);
        }

        [HttpGet("{id}/content")]
        public async Task<ActionResult<string>> GetContent(int id)
        {
            string? content = await _chapterService.GetContentAsync(id);
            if (content == null)
            {
                return NotFound(new { message = $"No content found for chapter {id}." });
            }
            return Ok(content);
        }

        [HttpPost("{id}/quiz/check")]
        [Authorize]
        public async Task<ActionResult<bool>> CheckAnswer(int id, [FromBody] CheckAnswerDto dto)
        {
            bool correct = await _chapterService.CheckAnswerAsync(dto.QuestionId, dto.Answer);
            return Ok(correct);
        }

        [HttpPost("{id}/quiz/result")]
        [Authorize]
        public async Task<ActionResult> SaveQuizResult(int id, [FromBody] SaveQuizResultDto dto)
        {
            string? userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            await _chapterService.SaveQuizResultAsync(id, userId, dto.Passed, dto.PerfectScore);
            return Ok();
        }
    }
}
