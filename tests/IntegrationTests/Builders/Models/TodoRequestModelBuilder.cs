using MyWebAPITemplate.Web.Models.RequestModels;

namespace MyWebAPITemplate.IntegrationTests.Builders.Models
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
