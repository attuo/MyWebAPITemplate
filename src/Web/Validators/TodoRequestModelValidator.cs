using AspNetCoreWebApiTemplate.Web.Models.RequestModels;
using FluentValidation;

namespace AspNetCoreWebApiTemplate.Web.Validators
{
    public class TodoRequestModelValidator : AbstractValidator<TodoRequestModel>
    {
        public TodoRequestModelValidator()
        {
            RuleFor(model => model.Description)
                .NotEmpty()
                .MaximumLength(100);

        }
    }
}