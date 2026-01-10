using EchoTrack.Api.Data;
using EchoTrack.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoTrack.Api.Repositories
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Feedback>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(f => f.CreatedBy == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Feedback>> GetByStatusAsync(string status)
        {
            return await _dbSet
                .Where(f => f.Status == status)
                .ToListAsync();
        }
    }
}
