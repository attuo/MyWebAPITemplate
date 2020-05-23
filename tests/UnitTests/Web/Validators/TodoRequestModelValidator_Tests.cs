using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreWebApiTemplate.UnitTests.Builders.Models;
using AspNetCoreWebApiTemplate.Web.Models.RequestModels;
using AspNetCoreWebApiTemplate.Web.Validators;
using FluentAssertions;
using FluentValidation.Results;
using Xunit;

namespace AspNetCoreWebApiTemplate.UnitTests.Web.Validators
{
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
            // 1.
            TodoRequestModel model = TodoRequestModelBuilder.CreateValid();

            // 2.
            ValidationResult result = Validator.Validate(model);

            // 3.
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Invalid_Model_Description_Null()
        {
            // 1.
            TodoRequestModel model = TodoRequestModelBuilder.CreateValid();
            model.Description = null;

            // 2.
            ValidationResult result = Validator.Validate(model);

            // 3.
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty().And.HaveCount(1);
        }

        [Fact]
        public void Invalid_Model_Description_Too_Long()
        {
            // 1.
            TodoRequestModel model = TodoRequestModelBuilder.CreateValid();
            model.Description = new string('A', 101);

            // 2.
            ValidationResult result = Validator.Validate(model);

            // 3.
            result.IsValid.Should().BeFalse();
            result.Errors.Should().NotBeEmpty().And.HaveCount(1);
            //result.Errors.Should().Contain(c => c.ErrorMessage.Contains("{The length of 'Description' must be 100 characters or fewer."));
        }
    }
}
