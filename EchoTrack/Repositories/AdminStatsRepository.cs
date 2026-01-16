using EchoTrack.Api.Data;
using EchoTrack.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EchoTrack.Api.Repositories
{
    public class AdminStatsRepository : IAdminStatsRepository
    {
        private readonly AppDbContext _context;

        public AdminStatsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminStats?> GetByAdminIdAsync(int adminUserId)
        {
            return await _context.AdminStats
                .FirstOrDefaultAsync(a => a.AdminUserId == adminUserId);
        }

        public async Task AddAsync(AdminStats stats)
        {
            await _context.AdminStats.AddAsync(stats);
        }

        public void Update(AdminStats stats)
        {
            _context.AdminStats.Update(stats);
        }
    }
}
