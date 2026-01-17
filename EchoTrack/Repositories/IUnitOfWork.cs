namespace EchoTrack.Api.Repositories
//implementation of UOW pattern
{
    public interface IUnitOfWork
    {
        IFeedbackRepository Feedbacks { get; }
        IAdminStatsRepository AdminStats { get; }
        Task<int> CommitAsync();
    }
}
