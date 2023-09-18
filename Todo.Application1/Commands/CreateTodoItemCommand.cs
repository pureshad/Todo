using MediatR;
using Todo.Infrastructure.Models;

public class CreateTodoItemCommand : CreateTodoItemCommandBase, IRequest<int>
{
}
