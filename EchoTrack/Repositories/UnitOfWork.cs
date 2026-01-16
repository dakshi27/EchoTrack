using EchoTrack.Api.Data;

namespace EchoTrack.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IFeedbackRepository Feedbacks { get; }
        public IAdminStatsRepository AdminStats { get; }

        public UnitOfWork(
            AppDbContext context,
            IFeedbackRepository feedbackRepository,
            IAdminStatsRepository adminStatsRepository)
        {
            _context = context;
            Feedbacks = feedbackRepository;
            AdminStats = adminStatsRepository;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
