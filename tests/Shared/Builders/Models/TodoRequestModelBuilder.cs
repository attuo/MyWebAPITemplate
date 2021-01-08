using MyWebAPITemplate.Source.Web.Models.RequestModels;

namespace MyWebAPITemplate.Tests.Shared.Builders.Models
{
    public static class TodoRequestModelBuilder
    {
        public static TodoRequestModel CreateValid()
        {
            return new TodoRequestModel
            {
                Description = "This is a valid todo",
                IsDone = true
            };
        }
    }
}
