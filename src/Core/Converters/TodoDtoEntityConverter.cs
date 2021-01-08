using System.Collections.Generic;
using System.Linq;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Core.Entities;
using MyWebAPITemplate.Source.Core.Interfaces.Converters;

namespace MyWebAPITemplate.Source.Core.Converters
{
    public class TodoDtoEntityConverter : ITodoDtoEntityConverter
    {
        /// <summary>
        /// Conversion from dto to entity does not include ID. It is always Guid.Empty on Entity when converted
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
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
