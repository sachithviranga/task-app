using TaskApp.Domain.Model;

namespace TaskApp.Application.Interface
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllAsync();
    }
}