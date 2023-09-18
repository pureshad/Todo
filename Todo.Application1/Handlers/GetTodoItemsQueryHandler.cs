using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo.Application.Database;
using Todo.Infrastructure.Models;

public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItemDto>>
{
    private readonly TodoContext _context;
    private readonly IMapper _mapper;

    public GetTodoItemsQueryHandler(TodoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItems = await _context.TodoItems.ToListAsync(cancellationToken);
        return _mapper.Map<List<TodoItemDto>>(todoItems);
    }
}