using MediatR;
using Todo.Infrastructure.Models;

public class GetTodoItemsQuery : IRequest<List<TodoItemDto>>
{
}
