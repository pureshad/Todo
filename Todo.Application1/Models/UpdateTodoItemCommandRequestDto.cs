namespace Todo.Application.Models
{
    public class UpdateTodoItemCommandRequestDto : CreateTodoItemCommandBase, IRequest
    {
        public bool? IsCompleted { get; set; }
    }
}
