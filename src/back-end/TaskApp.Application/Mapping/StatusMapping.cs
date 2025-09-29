using TaskApp.Domain.Model;
using TaskApp.Shared.DTO;

namespace TaskApp.Application.Mapping
{
    public static class StatusMapping
    {
        public static StatusDto ToDto(Status status)
        {
            return new StatusDto
            {
                Id = status.Id,
                Name = status.Name
            };
        }

    }
}
