namespace ApplicationCore.Entities
{
    public class TodoEntity : BaseEntity
    {
        public string TodoText { get; set; }

        public bool Done { get; set; }
    }
}