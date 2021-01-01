using System.Collections.Generic;
using System.Linq;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Web.Interfaces;
using MyWebAPITemplate.Web.Models.RequestModels;

namespace MyWebAPITemplate.Web.Converters
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
