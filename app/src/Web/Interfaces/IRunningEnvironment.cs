namespace MyWebAPITemplate.Source.Web.Interfaces;

/// <summary>
/// Represents the current running environment of the system.
/// </summary>
public interface IRunningEnvironment
{
    /// <summary>
    /// Gets the order of the environment.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Checks whether the running environment is any of the local environments.
    /// </summary>
    /// <returns>True if running on local environment.</returns>
    public bool IsLocalDevelopment();

    /// <summary>
    /// Checks whether the running environment is testing.
    /// </summary>
    /// <returns>True if running on testing environment.</returns>
    public bool IsTesting();
}