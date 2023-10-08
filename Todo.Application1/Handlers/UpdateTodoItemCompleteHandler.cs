namespace Todo.Application.Handlers
{
    public class UpdateTodoItemCompleteHandler : IRequestHandler<UpdateCompletedTodoItemCommand>
    {
        private readonly ITodoContext _context;

        public UpdateTodoItemCompleteHandler(ITodoContext context)
        {
            _context = context;
        }

        async Task IRequestHandler<UpdateCompletedTodoItemCommand>.Handle(UpdateCompletedTodoItemCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var todoItem = await _context.TodoItems.FindAsync(new object?[] { request.Id },
                    cancellationToken: cancellationToken)
                    ?? throw new Exception("Todo item not found.");

                todoItem.IsCompleted = request.IsCompleted;

                await _context.SaveChangesAsync(cancellationToken: cancellationToken);

            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating todo item. Message: {ex.InnerException?.Message ?? ex.Message}");
            }
        }
    }
}