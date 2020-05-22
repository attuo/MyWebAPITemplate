using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;
using AspNetCoreWebApiTemplate.Models.ResponseModels;
using AspNetCoreWebApiTemplate.Web.Interfaces;
using AspNetCoreWebApiTemplate.Web.Models.RequestModels;

namespace AspNetCoreWebApiTemplate.Web.Converters
{
    public class TodoModelDtoConverter : ITodoModelDtoConverter
    {
        public TodoDto Convert(TodoRequestModel model)
        {
            if (model == null) return null;
            return new TodoDto
            {
                Description = model.Description,
                IsDone = model.IsDone
            };
        }

        public TodoResponseModel Convert(TodoDto dto)
        {
            if (dto == null) return null;
            return new TodoResponseModel
            {
                Id = dto.Id,
                Description = dto.Description,
                IsDone = dto.IsDone
            };
        }

        public IEnumerable<TodoResponseModel> Convert(IEnumerable<TodoDto> dtos)
        {
            if (dtos == null) return null;
            return dtos.Select(dto => Convert(dto));
        }
    }
}
