using System.Collections.Generic;
using System.Linq;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Core.Entities;
using MyWebAPITemplate.Core.Interfaces.Converters;

namespace MyWebAPITemplate.Core.Converters
{
    public class TodoDtoEntityConverter : ITodoDtoEntityConverter
    {
        public TodoEntity Convert(TodoDto dto)
        {
            if (dto == null) return null;
            return new TodoEntity
            {
                Description = dto.Description,
                IsDone = dto.IsDone,
            };
        }

        public TodoEntity Convert(TodoDto dto, TodoEntity entity)
        {
            if (dto == null || entity == null) return null;
            entity.Description = dto.Description;
            entity.IsDone = dto.IsDone;
            return entity;
        }

        public TodoDto Convert(TodoEntity entity)
        {
            if (entity == null) return null;
            return new TodoDto
            {
                Id = entity.Id,
                Description = entity.Description,
                IsDone = entity.IsDone,
            };
        }

        public IEnumerable<TodoDto> Convert(IReadOnlyList<TodoEntity> entities)
        {
            if (entities == null) return null;
            return entities.Select(entity => Convert(entity));
        }
    }
}
