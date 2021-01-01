﻿using System.Collections.Generic;
using MyWebAPITemplate.Core.Dtos;
using MyWebAPITemplate.Models.ResponseModels;
using MyWebAPITemplate.Web.Models.RequestModels;

namespace MyWebAPITemplate.Web.Interfaces
{
    public interface ITodoModelDtoConverter
    {
        TodoDto Convert(TodoRequestModel model);
        TodoResponseModel Convert(TodoDto dto);
        IEnumerable<TodoResponseModel> Convert(IEnumerable<TodoDto> dtos);
    }
}
