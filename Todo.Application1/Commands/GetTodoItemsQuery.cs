using MediatR;
using Todo.Application.Models;

public class GetTodoItemsQuery : IRequest<List<TodoItemDto>>
{
}
