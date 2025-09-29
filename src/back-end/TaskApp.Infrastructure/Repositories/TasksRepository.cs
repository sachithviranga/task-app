using Microsoft.EntityFrameworkCore;
using TaskApp.Application.Interface;
using TaskApp.Domain.Model;
using TaskApp.Infrastructure.Data;
using TaskApp.Infrastructure.Entities;

namespace TaskApp.Infrastructure.Repositories
{
    public class TasksRepository : ITasksRepository
    {
        private readonly TaskAppDbContext _context;

        public TasksRepository(TaskAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tasks>> GetAllAsync()
        {
            var entities = await _context.Tasks
                .Include(t => t.Status)
                .ToListAsync();

            return entities.Select(MapToDomain);
        }

        public async Task<Tasks?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Tasks
                .Include(t => t.Status)
                .FirstOrDefaultAsync(t => t.Id == id);

            return entity != null ? MapToDomain(entity) : null;
        }

        public async Task<Tasks> CreateAsync(Tasks task)
        {
            var entity = MapToEntity(task);
            _context.Tasks.Add(entity);
            await _context.SaveChangesAsync();
            return MapToDomain(entity);
        }

        public async Task<Tasks> UpdateAsync(Tasks task)
        {
            var entity = await _context.Tasks.FindAsync(task.Id);
            if (entity == null)
                throw new ArgumentException("Task not found");

            entity.Title = task.Title;
            entity.StatusId = task.StatusId;
            entity.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return MapToDomain(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.Tasks.FindAsync(id);
            if (entity == null)
                return false;

            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        private static Tasks MapToDomain(TaskEntity entity)
        {
            return new Tasks
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                StatusId = entity.StatusId,
                Status = entity.Status != null ? new Status
                {
                    Id = entity.Status.Id,
                    Name = entity.Status.Name
                } : null,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        private static TaskEntity MapToEntity(Tasks domain)
        {
            return new TaskEntity
            {
                Id = domain.Id,
                Title = domain.Title,
                Description = domain.Description,
                StatusId = domain.StatusId,
                CreatedAt = domain.CreatedAt,
                UpdatedAt = domain.UpdatedAt
            };
        }
    }
}
