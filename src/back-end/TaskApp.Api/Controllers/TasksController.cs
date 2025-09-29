using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Interface;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
	/// <summary>
	///	CRUD endpoints for tasks.
	/// </summary>
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

		/// <summary>
		///	Creates a new <see cref="TasksController"/>.
		/// </summary>
		/// <param name="tasksService">Domain service for task operations.</param>
        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

		/// <summary>
		///	Gets all tasks.
		/// </summary>
		/// <returns>List of <see cref="TasksDto"/>.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksDto>>> GetAll()
        {
            var tasks = await _tasksService.GetAllAsync();
            return Ok(tasks);
        }

		/// <summary>
		///	Gets a task by identifier.
		/// </summary>
		/// <param name="id">Task identifier.</param>
		/// <returns>200-OK with task or 404 if not found.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TasksDto>> GetById(Guid id)
        {
            var task = await _tasksService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

		/// <summary>
		///	Creates a new task.
		/// </summary>
		/// <param name="createDto">Creation payload.</param>
		/// <returns>201-Created with location header, or 400 on validation errors.</returns>
        [HttpPost]
        public async Task<ActionResult<TasksDto>> Create(CreateTasksDto createDto)
        {
            try
            {
                var task = await _tasksService.CreateAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		/// <summary>
		///	Updates an existing task.
		/// </summary>
		/// <param name="id">Route identifier, must match body id.</param>
		/// <param name="updateDto">Update payload containing the identifier.</param>
		/// <returns>200-OK with updated task, 404 if not found, 400 on mismatch or validation errors.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TasksDto>> Update(Guid id, UpdateTasksDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest("ID mismatch");

            try
            {
                var task = await _tasksService.UpdateAsync(updateDto);
                return Ok(task);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

		/// <summary>
		///	Deletes a task.
		/// </summary>
		/// <param name="id">Task identifier.</param>
		/// <returns>204-NoContent on success or 404 if not found.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _tasksService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
