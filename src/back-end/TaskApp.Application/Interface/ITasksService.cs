using TaskApp.Shared.DTO;

namespace TaskApp.Application.Interface
{
	/// <summary>
	///	Encapsulates domain operations for managing tasks.
	/// </summary>
    public interface ITasksService
    {
		/// <summary>
		///	Gets all tasks.
		/// </summary>
        Task<IEnumerable<TasksDto>> GetAllAsync();
		/// <summary>
		///	Gets a task by its identifier.
		/// </summary>
		/// <param name="id">Task identifier.</param>
        Task<TasksDto?> GetByIdAsync(Guid id);
		/// <summary>
		///	Creates a new task.
		/// </summary>
		/// <param name="createDto">Creation payload.</param>
        Task<TasksDto> CreateAsync(CreateTasksDto createDto);
		/// <summary>
		///	Updates an existing task.
		/// </summary>
		/// <param name="updateDto">Update payload containing identifier.</param>
        Task<TasksDto> UpdateAsync(UpdateTasksDto updateDto);
		/// <summary>
		///	Deletes a task by its identifier.
		/// </summary>
		/// <param name="id">Task identifier.</param>
		/// <returns>True if deleted; otherwise false.</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
