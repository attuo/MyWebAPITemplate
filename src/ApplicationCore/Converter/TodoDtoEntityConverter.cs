using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;
using AspNetCoreWebApiTemplate.ApplicationCore.Entities;
using AspNetCoreWebApiTemplate.ApplicationCore.Interfaces.Converters;

namespace AspNetCoreWebApiTemplate.ApplicationCore.Converter
{
    public class TodoDtoEntityConverter : ITodoDtoEntityConverter
    {
        public TodoEntity Convert(TodoDto dto)
        {
            if (dto == null) return null;
            return new TodoEntity
            {
                //Id = dto.Id,
                Description = dto.Description,
                IsDone = dto.IsDone,
            };
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

        
    }
}
