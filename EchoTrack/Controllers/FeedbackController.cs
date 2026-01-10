using EchoTrack.Api.Models;
using EchoTrack.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EchoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        // GET: api/feedback
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return Ok(feedbacks);
        }

        // GET: api/feedback/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            return Ok(feedback);
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            await _feedbackRepository.AddAsync(feedback);
            return CreatedAtAction(nameof(GetById), new { id = feedback.Id }, feedback);
        }

        // PUT: api/feedback/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Feedback feedback)
        {
            if (id != feedback.Id)
                return BadRequest();

            await _feedbackRepository.UpdateAsync(feedback);
            return NoContent();
        }

        // DELETE: api/feedback/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            await _feedbackRepository.DeleteAsync(feedback);
            return NoContent();
        }
    }
}
