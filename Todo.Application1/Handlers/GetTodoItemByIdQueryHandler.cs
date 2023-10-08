public class GetTodoItemByIdQueryHandler : IRequestHandler<GetTodoItemByIdQuery, TodoItemDto>
{
    private readonly ITodoContext _context;

    public GetTodoItemByIdQueryHandler(ITodoContext context)
    {
        _context = context;
    }

    public async Task<TodoItemDto> Handle(GetTodoItemByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var todoItem = await _context.TodoItems.FindAsync(request.Id)
                ?? throw new Exception("Todo item not found.");

            // Map the TodoItem entity to a TodoItemDto if needed
            return new TodoItemDto
            {
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error fetching todo item by ID.", ex);
        }
    }
}
