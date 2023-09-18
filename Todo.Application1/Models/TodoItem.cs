namespace Todo.Infrastructure.Models
{
    public class TodoItem : CreateTodoItemCommandBase
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}