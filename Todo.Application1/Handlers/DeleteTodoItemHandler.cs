using MediatR;
using Todo.Application.Database;

public class DeleteTodoItemHandler : IRequestHandler<DeleteTodoItemCommand>
{
    private readonly TodoContext _context;

    public DeleteTodoItemHandler(TodoContext context)
    {
        _context = context;
    }

    async Task IRequestHandler<DeleteTodoItemCommand>.Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var todoItem = await _context.TodoItems.FindAsync(new object?[] { request.Id }, 
                cancellationToken: cancellationToken) ?? throw new Exception("Todo item not found.");
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // Handle the exception, log it, and possibly throw a custom exception
            throw new Exception("Error deleting todo item.", ex);
        }
    }
}