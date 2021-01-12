

namespace MyWebAPITemplate.Source.Web.Models.BaseModels
{
    /// <summary>
    /// Acts as base model for Todo request and response models
    /// Contains the shared attributes
    /// </summary>
    public abstract class TodoBaseModel
    {
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}
