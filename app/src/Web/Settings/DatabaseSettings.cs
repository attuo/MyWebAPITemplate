namespace MyWebAPITemplate.Source.Web.Settings;

/// <summary>
/// Includes the database settings
/// </summary>
public class DatabaseSettings
{
    public const string OptionsName = "Database";

    public string ConnectionString { get; set; }
}