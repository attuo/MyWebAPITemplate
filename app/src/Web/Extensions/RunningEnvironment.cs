using MyWebAPITemplate.Source.Web.Interfaces;

namespace MyWebAPITemplate.Source.Web.Extensions;

/// <summary>
/// Smart enumeration class that contains all the environments of the system.
/// </summary>
public sealed class RunningEnvironment : IRunningEnvironment
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

    /// <summary>
    /// Gets the order of the environment.
    /// </summary>
    public int Order { get; }

    /// <inheritdoc/>
    public string Name { get; }

    #region Environment definitions

    /// <summary>
    /// Gets all the environments of the system.
    /// This must appear before other static instance types.
    /// </summary>
    private static List<RunningEnvironment> AllEnvironments { get; } = new List<RunningEnvironment>();

    /// <summary>
    /// Gets a local development, which represents running the system without Docker locally.
    /// </summary>
    private static RunningEnvironment Local { get; } = new RunningEnvironment(0, nameof(Local));

    /// <summary>
    /// Gets a local development, which represents running the system in Docker locally.
    /// </summary>
    private static RunningEnvironment LocalDocker { get; } = new RunningEnvironment(1, nameof(LocalDocker));

    /// <summary>
    /// Gets a testing development, which represents running the system with a test configurations.
    /// For example automated testing purposes.
    /// </summary>
    public static RunningEnvironment Testing { get; } = new RunningEnvironment(3, nameof(Testing));

    /// <summary>
    /// Gets a QA development, which represents running the system in a remote QA server.
    /// </summary>
    private static RunningEnvironment QA { get; } = new RunningEnvironment(2, nameof(QA));

    /// <summary>
    /// Gets a QA development, which represents running the system in a remote Production server.
    /// </summary>
    private static RunningEnvironment Production { get; } = new RunningEnvironment(3, nameof(Production));

    #endregion Environment definitions

    #region Methods

    /// <summary>
    /// Checks if the environment exists with given name.
    /// </summary>
    /// <param name="name">Name of the environment.</param>
    /// <returns>True if exists.</returns>
    public static bool Exists(string name) =>
        AllEnvironments.Exists(c =>
            c.Name.Equals(name, StringComparison.Ordinal));

    /// <summary>
    /// Gets an environment with given name, if one exists.
    /// </summary>
    /// <param name="name">Name of the environment.</param>
    /// <returns>Matching environment if found.</returns>
    public static IRunningEnvironment? Get(string name) =>
        AllEnvironments.Find(c =>
            c.Name.Equals(name, StringComparison.Ordinal));

    /// <inheritdoc/>
    public bool IsLocalDevelopment()
        => Name.Equals(Local.Name, StringComparison.Ordinal) ||
           Name.Equals(LocalDocker.Name, StringComparison.Ordinal);

    /// <inheritdoc/>
    public bool IsTesting()
        => Name.Equals(Testing.Name, StringComparison.Ordinal);

    #endregion Methods
}