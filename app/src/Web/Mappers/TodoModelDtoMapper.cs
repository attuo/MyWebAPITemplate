using System;
using System.Collections.Generic;
using System.Linq;
using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Web.Interfaces.Mappers;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;

namespace MyWebAPITemplate.Source.Web.Mappers;

/// <inheritdoc/>
public class TodoModelDtoMapper : ITodoModelDtoMapper
{
    /// <inheritdoc/>
    public TodoDto Map(TodoRequestModel model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));

        return new TodoDto
        {
            Description = model.Description,
            IsDone = model.IsDone
        };
    }

    /// <inheritdoc/>
    public TodoResponseModel Map(TodoDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto, nameof(dto));

        return new TodoResponseModel
        {
            Id = dto.Id.GetValueOrDefault(),
            Description = dto.Description,
            IsDone = dto.IsDone
        };
    }

    /// <inheritdoc/>
    public IEnumerable<TodoResponseModel> Map(IEnumerable<TodoDto> dtos)
    {
        ArgumentNullException.ThrowIfNull(dtos, nameof(dtos));

        return dtos.Select(dto => Map(dto));
    }
}