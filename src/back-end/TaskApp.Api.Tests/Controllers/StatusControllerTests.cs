using Microsoft.AspNetCore.Mvc;
using Moq;
using TaskApp.Api.Controllers;
using TaskApp.Api.Tests.Data;
using TaskApp.Application.Interface;
using TaskApp.Shared.DTO;

namespace TaskApp.Api.Tests.Controllers
{
	/// <summary>
	///	Unit tests for <see cref="StatusController"/> endpoints.
	/// </summary>
	public class StatusControllerTests
	{
		/// <summary>
		///	Verifies that <see cref="StatusController.GetAll"/> returns 200-OK and the expected payload.
		/// </summary>
		[Fact]
		public async Task GetAll_ReturnsOk_WithStatuses()
		{
			// Arrange: create a mock of the domain service and seed known return data
			var mockService = new Mock<IStatusService>();

			mockService.Setup(s => s.GetAllAsync())
				.ReturnsAsync(TestData.Statuses);

			var controller = new StatusController(mockService.Object);

			// Act: call the controller action under test
			var result = await controller.GetAll();

			// Assert: ensure HTTP 200 and that the payload matches the seeded data
			var ok = Assert.IsType<OkObjectResult>(result.Result);
			var value = Assert.IsAssignableFrom<IEnumerable<StatusDto>>(ok.Value);
			Assert.Equal(TestData.Statuses, value);
		}
	}
}


