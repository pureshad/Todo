using Todo.Application.Models;

public class GetTodoItemByIdQuery : IRequest<TodoItemDto>
{
    public int Id { get; set; }
}
