using System.Collections.Generic;
using System.Linq;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Mappers;

namespace MyWebAPITemplate.Source.Core.Mappers
{
    ///<inheritdoc/>
    public class TodoDtoEntityMapper : ITodoDtoEntityMapper
    {
        public TodoEntity Map(TodoDto dto)
        {
            if (dto == null) return null;
            return new TodoEntity
            {
                Description = dto.Description,
                IsDone = dto.IsDone,
            };
        }

        public TodoEntity Map(TodoDto dto, TodoEntity entity)
        {
            if (dto == null || entity == null) return null;
            entity.Description = dto.Description;
            entity.IsDone = dto.IsDone;
            return entity;
        }

        public TodoDto Map(TodoEntity entity)
        {
            if (entity == null) return null;
            return new TodoDto
            {
                Id = entity.Id,
                Description = entity.Description,
                IsDone = entity.IsDone,
            };
        }

        public IEnumerable<TodoDto> Map(IReadOnlyList<TodoEntity> entities)
        {
            if (entities == null) return null;
            return entities.Select(entity => Map(entity));
        }
    }
}
