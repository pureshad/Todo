using MediatR;
using Todo.Application.Database;
using Todo.Infrastructure.Models;

public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly TodoContext _context;

    public CreateTodoItemHandler(TodoContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem { Title = request.Title };
        _context.TodoItems.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}