using System.Collections.Generic;

namespace MyWebAPITemplate.Source.Web.Extensions;

/// <summary>
/// Smart enumeration class that contains all the environments of the system.
/// </summary>
public sealed class RunningEnvironment
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RunningEnvironment"/> class.
    /// This is needed for the smart enumerations pattern.
    /// </summary>
    /// <param name="order">Order number of the environment.</param>
    /// <param name="name">Name of the environment.</param>
    private RunningEnvironment(int order, string name)
    {
        Order = order;
        Name = name;
        AllEnvironments.Add(this);
    }

    #region Environment definitions

    /// <summary>
    /// Gets all the environments of the system.
    /// This must appear before other static instance types.
    /// </summary>
    public static List<RunningEnvironment> AllEnvironments { get; } = new List<RunningEnvironment>();

    /// <summary>
    /// Gets a local development, which represents running the system without Docker locally.
    /// </summary>
    public static RunningEnvironment Local { get; } = new RunningEnvironment(0, nameof(Local));

    /// <summary>
    /// Gets a local development, which represents running the system in Docker locally.
    /// </summary>
    public static RunningEnvironment LocalDocker { get; } = new RunningEnvironment(1, nameof(LocalDocker));

    /// <summary>
    /// Gets a testing development, which represents running the system with a test configurations.
    /// For example automated testing purposes.
    /// </summary>
    public static RunningEnvironment Testing { get; } = new RunningEnvironment(3, nameof(Testing));

    /// <summary>
    /// Gets a QA development, which represents running the system in a remote QA server.
    /// </summary>
    public static RunningEnvironment QA { get; } = new RunningEnvironment(2, nameof(QA));

    /// <summary>
    /// Gets a QA development, which represents running the system in a remote Production server.
    /// </summary>
    public static RunningEnvironment Production { get; } = new RunningEnvironment(3, nameof(Production));

    #endregion Environment definitions

    /// <summary>
    /// Gets the order of the environment.
    /// </summary>
    public int Order { get; }

    /// <summary>
    /// Gets the name of the environment.
    /// </summary>
    public string Name { get; }

    #region Methods

    /// <summary>
    /// Checks if the environment exists with given name.
    /// </summary>
    /// <param name="name">Name of the environment.</param>
    /// <returns>True if exists.</returns>
    public static bool Exists(string name) =>
        AllEnvironments.Exists(c =>
            c.Name.Equals(name, System.StringComparison.Ordinal));

    /// <summary>
    /// Gets an environment with given name, if one exists.
    /// </summary>
    /// <param name="name">Name of the environment.</param>
    /// <returns>Matching environment if found.</returns>
    public static RunningEnvironment? Get(string name) =>
        AllEnvironments.Find(c =>
            c.Name.Equals(name, System.StringComparison.Ordinal));

    /// <summary>
    /// Checks whether the running environment is local.
    /// </summary>
    /// <returns>True if running on local environment.</returns>
    public bool IsLocalDevelopment() =>
        Name.Equals(Local.Name, System.StringComparison.Ordinal) ||
        Name.Equals(LocalDocker.Name, System.StringComparison.Ordinal);

    /// <summary>
    /// Checks whether the running environment is testing.
    /// </summary>
    /// <returns>True if running on testing environment.</returns>
    public bool IsTesting() => Name.Equals(Testing.Name, System.StringComparison.Ordinal);

    #endregion Methods
}