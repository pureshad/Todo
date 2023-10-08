public class CreateTodoItemHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly ITodoContext _context;

    public CreateTodoItemHandler(ITodoContext context)
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