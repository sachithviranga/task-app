using TaskApp.Shared.DTO;

namespace TaskApp.Api.Tests.Data
{
    public static class TestData
    {
        public static List<StatusDto> Statuses =
            [
                    new() {Id = 1,  Name = "To Do" },
                    new() {Id = 2 , Name = "In Progress" },
                    new() {Id = 3 , Name = "Completed" },
                    new() {Id = 4, Name = "Cancelled" }
            ];

        public static List<TasksDto> Tasks =
            [
                new TasksDto { Id = Guid.NewGuid(), Title = "Title1", Description = "Desc1", StatusId = 1 },
                new TasksDto { Id = Guid.NewGuid(), Title = "Title2", Description = "Desc2", StatusId = 1 }
            ];
    }
}
