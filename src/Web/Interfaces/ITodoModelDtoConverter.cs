using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreWebApiTemplate.ApplicationCore.Dtos;
using AspNetCoreWebApiTemplate.Models.ResponseModels;
using AspNetCoreWebApiTemplate.Web.Models.RequestModels;

namespace AspNetCoreWebApiTemplate.Web.Interfaces
{
    public interface ITodoModelDtoConverter
    {
        TodoDto Convert(TodoRequestModel model);
        TodoResponseModel Convert(TodoDto dto);
        IEnumerable<TodoResponseModel> Convert(IEnumerable<TodoDto> dtos);
    }
}
