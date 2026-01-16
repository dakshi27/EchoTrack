using Microsoft.EntityFrameworkCore;
using EchoTrack.Api.Models;
using EchoTrack.Api.Security;

namespace EchoTrack.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<AdminStats> AdminStats { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = PasswordHasher.Hash("admin123"),
                    Role = "Admin"
                },
                new User
                {
                    Id = 2,
                    Username = "user",
                    PasswordHash = PasswordHasher.Hash("user123"),
                    Role = "User"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
