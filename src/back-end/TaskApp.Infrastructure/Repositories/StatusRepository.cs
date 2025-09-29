using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Interface;
using TaskApp.Domain.Model;
using TaskApp.Infrastructure.Data;
using TaskApp.Infrastructure.Mapping;

namespace TaskApp.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly TaskAppDbContext _context;

        public StatusRepository(TaskAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            var entities = await _context.Statuses.ToListAsync();
            return entities.Select(StatusMapping.MapToDomain);
        }

    }
}