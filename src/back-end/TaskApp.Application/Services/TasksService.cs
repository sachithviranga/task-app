using Microsoft.Extensions.Logging;
using TaskApp.Application.Interface;
using TaskApp.Application.Mapping;
using TaskApp.Domain.Model;
using TaskApp.Shared.DTO;

namespace TaskApp.Application.Services
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _tasksRepository;
        private readonly ILogger<TasksService> _logger;

		public TasksService(ITasksRepository tasksRepository, ILogger<TasksService> logger)
        {
			_tasksRepository = tasksRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<TasksDto>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all tasks from repository");
			var tasks = await _tasksRepository.GetAllAsync();
            return tasks.Select(TasksMapping.MapToDto);
        }

        public async Task<TasksDto?> GetByIdAsync(Guid id)
        {
            _logger.LogDebug("Fetching task by id {Id}", id);
			var task = await _tasksRepository.GetByIdAsync(id);
            return task != null ? TasksMapping.MapToDto(task) : null;
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
            _logger.LogInformation("Creating task {Title}", createDto.Title);
            var createdTask = await _tasksRepository.CreateAsync(task);
            return TasksMapping.MapToDto(createdTask);
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
            _logger.LogInformation("Updating task {Id}", updateDto.Id);
            var updatedTask = await _tasksRepository.UpdateAsync(task);
            return TasksMapping.MapToDto(updatedTask);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            _logger.LogInformation("Deleting task {Id}", id);
			return await _tasksRepository.DeleteAsync(id);
        }


    }
}
