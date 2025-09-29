using TaskApp.Shared.DTO;

namespace TaskApp.Application.Interface
{
    public interface ITasksService
    {
        Task<IEnumerable<TasksDto>> GetAllAsync();
        Task<TasksDto?> GetByIdAsync(Guid id);
        Task<TasksDto> CreateAsync(CreateTasksDto createDto);
        Task<TasksDto> UpdateAsync(UpdateTasksDto updateDto);
        Task<bool> DeleteAsync(Guid id);
    }
}
