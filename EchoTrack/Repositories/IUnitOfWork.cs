namespace EchoTrack.Api.Repositories
{
    public interface IUnitOfWork
    {
        IFeedbackRepository Feedbacks { get; }
        IAdminStatsRepository AdminStats { get; }
        Task<int> CommitAsync();
    }
}
