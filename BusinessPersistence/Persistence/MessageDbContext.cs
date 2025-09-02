using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence.data;

namespace Persistence
{
    public class MessageDbContext : DbContext
    {
        private readonly string _connectionString;

        public MessageDbContext(DbContextOptions<MessageDbContext> options) : base(options)
        {
        }

        public MessageDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<MessageData> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!string.IsNullOrEmpty(_connectionString))
            {
                optionsBuilder.UseNpgsql(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageData>()
                .ToTable("messages")
                .HasKey(m => m.Id);
        }
    }
}
