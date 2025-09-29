using TaskApp.Application.Interface;
using TaskApp.Domain.Model;
using TaskApp.Shared.DTO;

namespace TaskApp.Application.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;

        public TasksService(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public async Task<IEnumerable<TasksDto>> GetAllAsync()
        {
            var tasks = await _tasksRepository.GetAllAsync();
            return tasks.Select(MapToDto);
        }

        public async Task<TasksDto?> GetByIdAsync(Guid id)
        {
            var task = await _tasksRepository.GetByIdAsync(id);
            return task != null ? MapToDto(task) : null;
        }

        public async Task<TasksDto> CreateAsync(CreateTasksDto createDto)
        {
            var task = new Tasks
            {
                Id = Guid.NewGuid(),
                Title = createDto.Title,
                StatusId = createDto.StatusId,
                Description = createDto.Description,
                CreatedAt = DateTime.UtcNow
            };

            var createdTask = await _tasksRepository.CreateAsync(task);
            return MapToDto(createdTask);
        }

        public async Task<TasksDto> UpdateAsync(UpdateTasksDto updateDto)
        {
            var task = new Tasks
            {
                Id = updateDto.Id,
                Title = updateDto.Title,
                Description = updateDto.Description,
                StatusId = updateDto.StatusId
            };

            var updatedTask = await _tasksRepository.UpdateAsync(task);
            return MapToDto(updatedTask);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _tasksRepository.DeleteAsync(id);
        }

        private TasksDto MapToDto(Tasks task)
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
