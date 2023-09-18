using MediatR;
using Todo.Application.Models;

public class CreateTodoItemCommand : CreateTodoItemCommandBase, IRequest<int>
{
}
