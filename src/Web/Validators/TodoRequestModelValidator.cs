using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                .MaximumLength(2);

            RuleFor(model => model.IsDone)
                .NotEmpty();
        }
    }
}