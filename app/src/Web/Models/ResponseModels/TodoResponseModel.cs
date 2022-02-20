using System;
using MyWebAPITemplate.Source.Web.Models.BaseModels;

namespace MyWebAPITemplate.Source.Web.Models.ResponseModels;

public class TodoResponseModel : TodoBaseModel
{
    public Guid Id { get; init; }
}