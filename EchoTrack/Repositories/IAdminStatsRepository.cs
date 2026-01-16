using EchoTrack.Api.Models;

namespace EchoTrack.Api.Repositories
{
    public interface IAdminStatsRepository
    {
        Task<AdminStats?> GetByAdminIdAsync(int adminUserId);
        Task AddAsync(AdminStats stats);
        void Update(AdminStats stats);
    }
}
