using MediatR;
using Todo.Infrastructure.Models;

public class GetTodoItemByIdQuery : IRequest<TodoItemDto>
{
    public int Id { get; set; }
}
