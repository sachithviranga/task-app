using TaskApp.Infrastructure.Data;
using TaskApp.Infrastructure.Entities;

namespace TaskApp.Api.Services
{
    public class DataSeedingService
    {
        private readonly TaskAppDbContext _context;

        public DataSeedingService(TaskAppDbContext context)
        {
            _context = context;
        }

        public async Task SeedStatusDataAsync()
        {
            if (!_context.Statuses.Any())
            {
                var statuses = new List<StatusEntity>
                {
                    new() {Id = 1,  Name = "To Do" },
                    new() {Id = 2 , Name = "In Progress" },
                    new() {Id = 3 , Name = "Completed" },
                    new() {Id = 4, Name = "Cancelled" }
                };

                _context.Statuses.AddRange(statuses);
                await _context.SaveChangesAsync();
            }
        }
    }
}
