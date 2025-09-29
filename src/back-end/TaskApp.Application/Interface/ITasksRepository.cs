using TaskApp.Domain.Model;

namespace TaskApp.Application.Interface
{
    public interface ITasksRepository
    {
        Task<IEnumerable<Tasks>> GetAllAsync();
        Task<Tasks?> GetByIdAsync(Guid id);
        Task<Tasks> CreateAsync(Tasks task);
        Task<Tasks> UpdateAsync(Tasks task);
        Task<bool> DeleteAsync(Guid id);
    }
}
