using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence
{
    public class MessageDbContextFactory : IDesignTimeDbContextFactory<MessageDbContext>
    {
        public MessageDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetSection("DbSettings:ConnectionString").Value;

            var optionsBuilder = new DbContextOptionsBuilder<MessageDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new MessageDbContext(optionsBuilder.Options);
        }
    }
}
