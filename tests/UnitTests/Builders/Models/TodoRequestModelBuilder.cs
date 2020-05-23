using System;
using System.Collections.Generic;
using System.Text;
using AspNetCoreWebApiTemplate.Web.Models.RequestModels;

namespace AspNetCoreWebApiTemplate.UnitTests.Builders.Models
{
    public static class TodoRequestModelBuilder
    {
        public static TodoRequestModel CreateValid()
        {
            return new TodoRequestModel
            {
                Description = "This is a valid todo",
                IsDone = false
            };
        }
    }
}
