using TaskApp.Application.Interface;
using TaskApp.Domain.Model;
using TaskApp.Shared.DTO;

namespace TaskApp.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;

        public StatusService(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<IEnumerable<StatusDto>> GetAllAsync()
        {
            var statuses = await _statusRepository.GetAllAsync();
            return statuses.Select(MapToDto);
        }

        private static StatusDto MapToDto(Status status)
        {
            return new StatusDto
            {
                Id = status.Id,
                Name = status.Name
            };
        }
    }
}