using FluentAssertions;
using FluentValidation.Results;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Validators;
using MyWebAPITemplate.Tests.Shared.Builders.Models;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Tests.Web.Validators;

/// <summary>
/// All the TodoRequestModelValidator tests.
/// </summary>
public class TodoRequestModelValidator_Tests
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TodoRequestModelValidator_Tests"/> class.
    /// </summary>
    public TodoRequestModelValidator_Tests()
    {
        Validator = new TodoRequestModelValidator();
    }

    private TodoRequestModelValidator Validator { get; }

    /// <summary>
    /// Happy case for model validation.
    /// </summary>
    [Fact]
    public void Valid_Model()
    {
        // Arrange
        TodoRequestModel model = TodoRequestModelBuilder.CreateValid();

        // Act
        ValidationResult result = Validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    /// <summary>
    /// Unhappy case for model validation.
    /// </summary>
    [Fact]
    public void Invalid_Model_Description_Null()
    {
        // Arrange
        TodoRequestModel model = TodoRequestModelBuilder.CreateValid();
        model.Description = null;

        // Act
        ValidationResult result = Validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty().And.HaveCount(1);
    }

    /// <summary>
    /// Unhappy case for model validation.
    /// </summary>
    [Fact]
    public void Invalid_Model_Description_Too_Long()
    {
        // Arrange
        TodoRequestModel model = TodoRequestModelBuilder.CreateValid();
        model.Description = new string('A', 101);

        // Act
        ValidationResult result = Validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeEmpty().And.HaveCount(1);
        // TODO: Check the error message too.
    }
}