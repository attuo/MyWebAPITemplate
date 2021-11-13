using System;
using MyWebAPITemplate.Source.Web.Models.BaseModels;

namespace MyWebAPITemplate.Source.Models.ResponseModels;

public class TodoResponseModel : TodoBaseModel
{
    public Guid Id { get; init; }
}
