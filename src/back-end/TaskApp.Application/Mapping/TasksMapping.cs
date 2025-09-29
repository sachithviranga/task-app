using TaskApp.Domain.Model;
using TaskApp.Shared.DTO;

namespace TaskApp.Application.Mapping
{
    public static class TasksMapping
    {
        public static TasksDto MapToDto(Tasks task)
        {
            return new TasksDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                StatusId = task.StatusId,
                StatusName = task.Status?.Name ?? string.Empty,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}
