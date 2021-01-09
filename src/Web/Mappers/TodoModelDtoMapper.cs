using System.Collections.Generic;
using System.Linq;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Web.Interfaces.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;

namespace MyWebAPITemplate.Source.Web.Mappers
{
    ///<inheritdoc/>
    public class TodoModelDtoMapper : ITodoModelDtoMapper
    {
        public TodoDto Map(TodoRequestModel model)
        {
            if (model == null) return null;
            return new TodoDto
            {
                Description = model.Description,
                IsDone = model.IsDone
            };
        }

        public TodoResponseModel Map(TodoDto dto)
        {
            if (dto == null) return null;
            return new TodoResponseModel
            {
                Id = dto.Id.GetValueOrDefault(),
                Description = dto.Description,
                IsDone = dto.IsDone
            };
        }

        public IEnumerable<TodoResponseModel> Map(IEnumerable<TodoDto> dtos)
        {
            if (dtos == null) return null;
            return dtos.Select(dto => Map(dto));
        }
    }
}
