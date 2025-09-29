using Microsoft.AspNetCore.Mvc;
using TaskApp.Application.Interface;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TasksDto>>> GetAll()
        {
            var tasks = await _tasksService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TasksDto>> GetById(Guid id)
        {
            var task = await _tasksService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

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
