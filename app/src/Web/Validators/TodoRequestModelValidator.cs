using FluentValidation;
using MyWebAPITemplate.Source.Web.Models.RequestModels;

namespace MyWebAPITemplate.Source.Web.Validators;

/// <summary>
/// Contains all the validation rules for Todo request models.
/// </summary>
public class TodoRequestModelValidator : AbstractValidator<TodoRequestModel>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TodoRequestModelValidator"/> class.
    /// </summary>
    public TodoRequestModelValidator()
    {
        RuleFor(model => model.Description)
            .NotEmpty()
            .MaximumLength(100);
    }
}