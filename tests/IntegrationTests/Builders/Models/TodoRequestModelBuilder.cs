using AspNetCoreWebApiTemplate.Web.Models.RequestModels;

namespace AspNetCoreWebApiTemplate.IntegrationTests.Builders.Models
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
