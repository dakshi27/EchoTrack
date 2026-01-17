using EchoTrack.Api.Models;
using EchoTrack.Api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EchoTrack.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FeedbackController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public FeedbackController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

            await _unitOfWork.Feedbacks.AddAsync(feedback);
            await _unitOfWork.CommitAsync();

            return CreatedAtAction(nameof(GetById), new { id = feedback.Id }, feedback);
        }

        /// <summary>
        /// User: Get feedback created by logged-in user
        /// </summary>
        [HttpGet("my")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetMyFeedback()
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var feedbacks = await _unitOfWork.Feedbacks.GetByUserIdAsync(userId);

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
            var feedbacks = await _unitOfWork.Feedbacks.GetAllAsync();
            return Ok(feedbacks);
        }

        /// <summary>
        /// Admin: Get feedback by id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(id);
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

            _unitOfWork.Feedbacks.Update(feedback);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        /// <summary>
        /// Admin: Delete feedback
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            _unitOfWork.Feedbacks.Delete(feedback);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        /// <summary>
        /// Admin: Close feedback (Phase 2 entry point)
        /// </summary>
        [HttpPut("{id}/close")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CloseFeedback(int id)
        {
            var feedback = await _unitOfWork.Feedbacks.GetByIdAsync(id);
            if (feedback == null)
                return NotFound();

            feedback.Status = "Closed";
            _unitOfWork.Feedbacks.Update(feedback);

            var adminUserId = int.Parse(User.FindFirst("UserId")!.Value);

            var stats = await _unitOfWork.AdminStats.GetByAdminIdAsync(adminUserId);
            if (stats == null)
            {
                stats = new AdminStats
                {
                    AdminUserId = adminUserId,
                    ClosedFeedbackCount = 1
                };
                await _unitOfWork.AdminStats.AddAsync(stats);
            }
            else
            {
                stats.ClosedFeedbackCount++;
                _unitOfWork.AdminStats.Update(stats);
            }

            await _unitOfWork.CommitAsync();
            return NoContent();
        }
    }
}
