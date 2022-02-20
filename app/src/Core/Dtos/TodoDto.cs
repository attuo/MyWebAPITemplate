using System;

namespace MyWebAPITemplate.Source.Core.Dtos;

public class TodoDto
{
    public Guid? Id { get; init; }
    public string Description { get; init; }
    public bool IsDone { get; init; }
}