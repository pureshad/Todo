using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItemDto>>
{
    private readonly ITodoContext _context;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly ILogger<GetTodoItemsQueryHandler> _logger;
    public GetTodoItemsQueryHandler(ITodoContext context, IMapper mapper,
        IMemoryCache memoryCache, ILogger<GetTodoItemsQueryHandler> logger)
    {
        _context = context;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _logger = logger;
    }

    public async Task<List<TodoItemDto>> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        string key = "ALL_LIST_ITEMS";
        _logger.LogError("DONE");
        var cachedItems = _memoryCache.Get<List<TodoItemDto>>(key);
        if (cachedItems != null)
            return cachedItems;

        var todoItems = await _context.TodoItems.ToListAsync(cancellationToken);
        if (todoItems?.Any() == true)
        {
            var result = _mapper.Map<List<TodoItemDto>>(todoItems);
            _memoryCache.Set(key, result, DateTimeOffset.Now.AddSeconds(60));
            return result;
        }
        _logger.LogWarning($"{nameof(Handle)} No items found id Db");
        return null!;
    }
}