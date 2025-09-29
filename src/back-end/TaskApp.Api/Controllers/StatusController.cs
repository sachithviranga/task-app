using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskApp.Application.Interface;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	/// <summary>
	///	Exposes read-only endpoints for task status lookup.
	/// </summary>
    public class StatusController : ControllerBase
    {
		private readonly IStatusService _statusService;
		private readonly ILogger<StatusController> _logger;

		/// <summary>
		///	Creates a new <see cref="StatusController"/>.
		/// </summary>
		/// <param name="statusService">Domain service that provides status data.</param>
		public StatusController(IStatusService statusService, ILogger<StatusController> logger)
        {
			_statusService = statusService;
			_logger = logger;
        }

		/// <summary>
		///	Gets all available statuses.
		/// </summary>
		/// <returns>List of <see cref="StatusDto"/> wrapped in 200-OK.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusDto>>> GetAll()
        {
			_logger.LogInformation("Handling {Action}", nameof(GetAll));
			var statuses = await _statusService.GetAllAsync();
            return Ok(statuses);
        }
    }
}