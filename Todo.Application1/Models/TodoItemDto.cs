namespace Todo.Application.Models
{
    public class TodoItemDto : CreateTodoItemCommandBase
    {
        public bool IsCompleted { get; set; }
    }
}