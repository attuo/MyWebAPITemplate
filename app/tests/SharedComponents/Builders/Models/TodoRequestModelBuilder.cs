using MyWebAPITemplate.Source.Web.Models.RequestModels;

namespace MyWebAPITemplate.Tests.SharedComponents.Builders.Models;

/// <summary>
/// TodoRequestModel builder.
/// </summary>
public static class TodoRequestModelBuilder
{
    /// <summary>
    /// Create valid model.
    /// </summary>
    /// <returns>Valid model.</returns>
    public static TodoRequestModel CreateValid()
    {
        return new TodoRequestModel
        {
            Description = "This is a valid todo",
            IsDone = true
        };
    }
}