using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Todo.Infrastructure.Models;

public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly TodoDbContext _context;

    public CreateTodoItemHandler(TodoDbContext context)
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

// Implement UpdateTodoItemHandler and DeleteTodoItemHandler similarly
