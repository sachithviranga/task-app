using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskApp.Domain.Model;
using TaskApp.Infrastructure.Entities;

namespace TaskApp.Infrastructure.Mapping
{
    public static class StatusMapping
    {
        public static Status MapToDomain(StatusEntity entity)
        {
            return new Status
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static StatusEntity MapToEntity(Status domain)
        {
            return new StatusEntity
            {
                Id = domain.Id,
                Name = domain.Name
            };
        }
    }
}
