namespace MyWebAPITemplate.Source.Core.Entities
{
    public class TodoEntity : BaseEntity
    {
        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}