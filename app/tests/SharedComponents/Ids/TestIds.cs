namespace MyWebAPITemplate.Tests.SharedComponents.Ids;

/// <summary>
/// Contains ids for test using.
/// Use those for database seeding etc.
/// </summary>
public static class TestIds
{
    /// <summary>
    /// For example for getting and updating.
    /// Use on database seeding.
    /// </summary>
    public static readonly Guid NormalUsageId = Guid.Parse("11111111-1111-1111-1111-111111111111"); // TODO: Think a better property name

    /// <summary>
    /// For example for deleting.
    /// Use on database seeding.
    /// </summary>
    public static readonly Guid OtherUsageId = Guid.Parse("22222222-2222-2222-2222-222222222222"); // TODO: Think a better property name

    /// <summary>
    /// For example for not found.
    /// Don't use on database seeding.
    /// </summary>
    public static readonly Guid NonUsageId = Guid.Parse("99999999-9999-9999-9999-999999999999"); // TODO: Think a better property name
}