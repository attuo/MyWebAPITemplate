using System;

namespace MyWebAPITemplate.Models.ResponseModels
{
    public class TodoResponseModel
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
        public bool IsDone { get; init; }
    }
}
