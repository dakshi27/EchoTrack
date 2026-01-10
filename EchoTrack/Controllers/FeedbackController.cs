using EchoTrack.Api.Models;
using EchoTrack.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // All endpoints require authentication
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        // =========================
        // USER ENDPOINTS
        // =========================

        /// <summary>
        /// User: Create new feedback
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(Feedback feedback)
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);

            feedback.CreatedBy = userId;
            feedback.CreatedAt = DateTime.UtcNow;
            feedback.Status = "Open";

            await _feedbackRepository.AddAsync(feedback);
            return CreatedAtAction(nameof(GetById), new { id = feedback.Id }, feedback);
        }

        /// <summary>
        /// User: Get feedback created by the logged-in user
        /// </summary>
        [HttpGet("my")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyFeedback()
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var feedbacks = await _feedbackRepository.GetByUserIdAsync(userId);

            return Ok(feedbacks);
        }

        // =========================
        // ADMIN ENDPOINTS
        // =========================

        /// <summary>
        /// Admin: View all feedback
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var feedbacks = await _feedbackRepository.GetAllAsync();
            return Ok(feedbacks);
        }

        /// <summary>
        /// Admin: Get feedback by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            return Ok(feedback);
        }

        /// <summary>
        /// Admin: Update feedback
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, Feedback feedback)
        {
            if (id != feedback.Id)
                return BadRequest();

            await _feedbackRepository.UpdateAsync(feedback);
            return NoContent();
        }

        /// <summary>
        /// Admin: Delete feedback
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            await _feedbackRepository.DeleteAsync(feedback);
            return NoContent();
        }

        /// <summary>
        /// Admin: Close feedback
        /// </summary>
        [HttpPut("{id}/close")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CloseFeedback(int id)
        {
            var feedback = await _feedbackRepository.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            feedback.Status = "Closed";
            await _feedbackRepository.UpdateAsync(feedback);

            return NoContent();
        }
    }
}
