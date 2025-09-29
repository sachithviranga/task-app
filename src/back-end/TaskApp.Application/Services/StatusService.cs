using Microsoft.Extensions.Logging;
using TaskApp.Application.Interface;
using TaskApp.Domain.Model;
using TaskApp.Shared.DTO;

namespace TaskApp.Application.Services
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly ILogger<StatusService> _logger;

		public StatusService(IStatusRepository statusRepository, ILogger<StatusService> logger)
        {
			_statusRepository = statusRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<StatusDto>> GetAllAsync()
        {
            _logger.LogDebug("Fetching all statuses from repository");
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