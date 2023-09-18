using MediatR;
using Todo.Application.Database;

public class UpdateTodoItemHandler : IRequestHandler<UpdateTodoItemCommand>
{
    private readonly TodoContext _context;

    public UpdateTodoItemHandler(TodoContext context)
    {
        _context = context;
    }
    async Task IRequestHandler<UpdateTodoItemCommand>.Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var todoItem = await _context.TodoItems.FindAsync(new object?[] { request.Id },
                cancellationToken: cancellationToken)
                ?? throw new Exception("Todo item not found.");

            todoItem.Title = request.Title;
            todoItem.IsCompleted = request.IsCompleted;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception("Error updating todo item.", ex);
        }
    }
}
