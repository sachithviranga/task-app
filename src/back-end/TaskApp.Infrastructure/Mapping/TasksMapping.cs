using TaskApp.Domain.Model;
using TaskApp.Infrastructure.Entities;

namespace TaskApp.Infrastructure.Mapping
{
    public static class TasksMapping
    {
        public static Tasks MapToDomain(TaskEntity entity)
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

        public static TaskEntity MapToEntity(Tasks domain)
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
