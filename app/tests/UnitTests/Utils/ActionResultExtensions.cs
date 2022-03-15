using Microsoft.AspNetCore.Mvc;

namespace MyWebAPITemplate.Tests.UnitTests.Utils;

/// <summary>
/// Extension methods for ActionResult.
/// </summary>
public static class ActionResultExtensions
{
    /// <summary>
    /// Extension method for getting the ActionResult's content value
    /// This is helper method, because getting the content from ActionResult
    /// is kinda tedious job to do.
    /// Read more: https://stackoverflow.com/questions/51489111/how-to-unit-test-with-actionresultt.
    /// </summary>
    /// <typeparam name="T">See <see cref="ActionResult{TValue}"/>.</typeparam>
    /// <param name="result">Extended action result.</param>
    /// <returns>Object result if the is one.</returns>
    public static T? GetObjectResult<T>(this ActionResult<T> result)
    {
        if (result == null)
            return default;

        return result.Result != null
            ? (T?)(result.Result as ObjectResult)?.Value
            : result.Value;
    }
}