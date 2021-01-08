using System.Collections.Generic;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Web.Models.RequestModels;

namespace MyWebAPITemplate.Source.Web.Interfaces.Mappers
{
    public interface ITodoModelDtoMapper
    {
        TodoDto Map(TodoRequestModel model);
        TodoResponseModel Map(TodoDto dto);
        IEnumerable<TodoResponseModel> Map(IEnumerable<TodoDto> dtos);
    }
}
