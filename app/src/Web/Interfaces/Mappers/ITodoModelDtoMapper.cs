using MyWebAPITemplate.Source.Core.Dtos;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Models.ResponseModels;

namespace MyWebAPITemplate.Source.Web.Interfaces.Mappers;

/// <summary>
/// Mapping methods between TodoModels and TodoDtos.
/// </summary>
public interface ITodoModelDtoMapper
{
    /// <summary>
    /// TodoRequestModel -> TodoDto
    /// Mapping from dto to entity should not include ID.
    /// It is always Guid.Empty on Entity when mapped.
    /// </summary>
    /// <param name="model">Model to be mapped from.</param>
    /// <returns>Dto that's been mapped to.</returns>
    TodoDto Map(TodoRequestModel model);

    /// <summary>
    /// TodoDto -> TodoResponseModel.
    /// </summary>
    /// <param name="dto">Dto to be mapped from.</param>
    /// <returns>Model that's been mapped to.</returns>
    TodoResponseModel Map(TodoDto dto);

    /// <summary>
    /// TodoDtos -> TodoResponseModels.
    /// </summary>
    /// <param name="dtos">Dtos to be mapped from.</param>
    /// <returns>Models that's been mapped to.</returns>
    IEnumerable<TodoResponseModel> Map(IEnumerable<TodoDto> dtos);
}