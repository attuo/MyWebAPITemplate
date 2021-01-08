using System.Collections.Generic;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Source.Web.Models.RequestModels;

namespace MyWebAPITemplate.Source.Web.Interfaces
{
    public interface ITodoModelDtoConverter
    {
        TodoDto Convert(TodoRequestModel model);
        TodoResponseModel Convert(TodoDto dto);
        IEnumerable<TodoResponseModel> Convert(IEnumerable<TodoDto> dtos);
    }
}
