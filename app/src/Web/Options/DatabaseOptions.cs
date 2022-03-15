using System.ComponentModel.DataAnnotations;

namespace MyWebAPITemplate.Source.Web.Options;

/// <summary>
/// Option pattern that includes the database related configuration settings.
/// </summary>
public class DatabaseOptions
{
    /// <summary>
    /// Name of the section.
    /// </summary>
    public const string OptionsName = "Database";

    /// <summary>
    /// Gets or sets database connection string.
    /// </summary>
    [Required]
    public string? ConnectionString { get; set; }
}