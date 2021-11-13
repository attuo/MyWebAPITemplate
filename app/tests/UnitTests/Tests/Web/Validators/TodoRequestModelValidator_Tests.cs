using FluentAssertions;
using FluentValidation.Results;
using MyWebAPITemplate.Source.Web.Models.RequestModels;
using MyWebAPITemplate.Source.Web.Validators;
using MyWebAPITemplate.Tests.Shared.Builders.Models;
using Xunit;

namespace MyWebAPITemplate.Tests.UnitTests.Web.Validators;

/// <summary>
/// All the TodoRequestModelValidator tests
/// </summary>
public class TodoRequestModelValidator_Tests
{
    private TodoRequestModelValidator Validator { get; }
    public TodoRequestModelValidator_Tests()
    {
        Validator = new TodoRequestModelValidator();
    }

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
        //result.Errors.Should().Contain(c => c.ErrorMessage.Contains("{The length of 'Description' must be 100 characters or fewer."));
    }
}
