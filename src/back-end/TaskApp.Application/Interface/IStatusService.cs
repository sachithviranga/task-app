using TaskApp.Shared.DTO;

namespace TaskApp.Application.Interface
{
	/// <summary>
	///	Provides read-only access to task status data for the API layer.
	/// </summary>
    public interface IStatusService
    {
		/// <summary>
		///	Gets all statuses available in the system.
		/// </summary>
		/// <returns>Enumeration of <see cref="StatusDto"/>.</returns>
        Task<IEnumerable<StatusDto>> GetAllAsync();
    }
}
