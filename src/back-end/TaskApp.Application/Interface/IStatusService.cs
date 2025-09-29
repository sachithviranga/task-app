using TaskApp.Shared.DTO;

namespace TaskApp.Application.Interface
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusDto>> GetAllAsync();
    }
}
