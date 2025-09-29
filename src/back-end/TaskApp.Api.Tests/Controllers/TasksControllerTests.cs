using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TaskApp.Api.Controllers;
using TaskApp.Api.Tests.Data;
using TaskApp.Application.Interface;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Tests.Controllers
{
	/// <summary>
	///	Unit tests for <see cref="TasksController"/> endpoints covering success and error cases.
	/// </summary>
	public class TasksControllerTests
	{

        private readonly Mock<ILogger<TasksController>> _loggerMock = new();
        private readonly Mock<ITasksService> _mockService = new();
        private TasksController _controller => new(_mockService.Object, _loggerMock.Object);

        /// <summary>
        ///	Ensures <see cref="TasksController.GetAll"/> returns 200-OK with the service data.
        /// </summary>
        [Fact]
		public async Task GetAll_ReturnsOk_WithTasks()
		{
            // Arrange: mock service to return a known set of tasks
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(TestData.Tasks);



			// Act
			var result = await _controller.GetAll();

			// Assert
			var ok = Assert.IsType<OkObjectResult>(result.Result);
			var value = Assert.IsAssignableFrom<IEnumerable<TasksDto>>(ok.Value);
			Assert.Equal(TestData.Tasks, value);
		}

		/// <summary>
		///	Ensures 404-NotFound is returned when a task cannot be located.
		/// </summary>
		[Fact]
		public async Task GetById_NotFound_WhenMissing()
		{
			// Arrange
			_mockService.Setup(s => s.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((TasksDto?)null);

			// Act
			var result = await _controller.GetById(Guid.NewGuid());

			// Assert
			Assert.IsType<NotFoundResult>(result.Result);
		}

		/// <summary>
		///	Ensures successful creation returns 201-Created and correct action/linking.
		/// </summary>
		[Fact]
		public async Task Create_ReturnsCreatedAt_WithTask()
		{
			// Arrange
			var create = new CreateTasksDto { Title = "Title", Description = "Desc", StatusId = 1 };
			var created = new TasksDto { Id = Guid.NewGuid(), Title = create.Title, Description = create.Description, StatusId = create.StatusId };
			_mockService.Setup(s => s.CreateAsync(create)).ReturnsAsync(created);


			// Act
			var result = await _controller.Create(create);

			// Assert
			var createdAt = Assert.IsType<CreatedAtActionResult>(result.Result);
			Assert.Equal(nameof(TasksController.GetById), createdAt.ActionName);
			Assert.Equal(created, createdAt.Value);
		}

		/// <summary>
		///	Validates that mismatched route/body IDs yield 400-BadRequest.
		/// </summary>
		[Fact]
		public async Task Update_ReturnsBadRequest_OnIdMismatch()
		{
			// Arrange
			var dto = new UpdateTasksDto { Id = Guid.NewGuid(), Title = "Title", Description = "Desc", StatusId = 2 };

			// Act
			var result = await _controller.Update(Guid.NewGuid(), dto);

			// Assert
			var bad = Assert.IsType<BadRequestObjectResult>(result.Result);
			Assert.Equal("ID mismatch", bad.Value);
		}

		/// <summary>
		///	Ensures deletion success returns 204-NoContent.
		/// </summary>
		[Fact]
		public async Task Delete_ReturnsNoContent_OnSuccess()
		{
			// Arrange
			_mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(true);

			// Act
			var result = await _controller.Delete(Guid.NewGuid());

			// Assert
			Assert.IsType<NoContentResult>(result);
		}

		/// <summary>
		///	Ensures deletion failure returns 404-NotFound.
		/// </summary>
		[Fact]
		public async Task Delete_ReturnsNotFound_OnFailure()
		{
			// Arrange
			_mockService.Setup(s => s.DeleteAsync(It.IsAny<Guid>())).ReturnsAsync(false);

			// Act
			var result = await _controller.Delete(Guid.NewGuid());

			// Assert
			Assert.IsType<NotFoundResult>(result);
		}
	}
}


