namespace Todo.Infrastructure.Models
{
    public class TodoItemDto : CreateTodoItemCommandBase
    {
        public bool IsCompleted { get; set; }
    }
}