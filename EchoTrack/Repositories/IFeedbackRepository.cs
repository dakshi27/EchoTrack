using EchoTrack.Api.Models;

namespace EchoTrack.Api.Repositories
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        Task<IEnumerable<Feedback>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Feedback>> GetByStatusAsync(string status);
    }
}
