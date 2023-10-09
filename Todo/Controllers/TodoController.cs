[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMemoryCache _memoryCache;

    private readonly ILogger<TodoController> _logger;

    public TodoController(IMediator mediator, ILogger<TodoController> logger, IMemoryCache memoryCache)
    {
        _mediator = mediator;
        _logger = logger;
        _memoryCache = memoryCache;
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTodoItem(CreateTodoItemCommand command)
    {
        try
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }
        catch (Exception ex)
        {
            string message = $"An error occurred while creating the todo item. Message {ex.InnerException?.Message ?? ex.Message}";
            _logger.LogError(message);
            return StatusCode(500, message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(int id, UpdateTodoItemCommandRequestDto command)
    {
        try
        {
            UpdateTodoItemCommand request = new() { Id = id, IsCompleted = command.IsCompleted, Title = command.Title };
            await _mediator.Send(request);
            return NoContent();
        }
        catch (Exception ex)
        {
            string message = $"An error occurred while updating the todo item. Message {ex.InnerException?.Message ?? ex.Message}";
            _logger.LogError(message);
            return StatusCode(500, message);
        }
    }

    [HttpPut("complete/{id}")]
    public async Task<IActionResult> UpdateTodoItemCompleted(int id, UpdateCompletedTodoItemCommandDto command)
    {
        try
        {
            UpdateCompletedTodoItemCommand request = new() { Id = id, IsCompleted = command.IsCompleted };
            await _mediator.Send(request);
            return NoContent();
        }
        catch (Exception ex)
        {
            string message = $"An error occurred while updating the todo item. Message {ex.InnerException?.Message ?? ex.Message}";
            _logger.LogError(message);
            return StatusCode(500, message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoItem(int id)
    {
        try
        {
            var command = new DeleteTodoItemCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            string message = $"An error occurred while deleting the todo item. Message {ex.InnerException?.Message ?? ex.Message}";
            _logger.LogError(message);
            return StatusCode(500, message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItemDto>>> GetTodoItems()
    {
        try
        {
            var query = new GetTodoItemsQuery();
            var items = await _mediator.Send(query);
            if (items?.Any() == true)
                return Ok(items);
            else
                return NotFound();
        }
        catch (Exception ex)
        {
            string message = $"An error occurred while fetching todo items. Message {ex.InnerException?.Message ?? ex.Message}";
            _logger.LogError(message);
            return StatusCode(500, message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetTodoItemById(int id)
    {
        try
        {
            var query = new GetTodoItemByIdQuery { Id = id };
            var item = await _mediator.Send(query);

            if (item == null)
            {
                _logger.LogWarning($"{nameof(GetTodoItemById)} Unable to find item by id: {id}");
                return NotFound();
            }

            return Ok(item);
        }
        catch (Exception ex)
        {
            string message = $"An error occurred while fetching the todo item. Message {ex.InnerException?.Message ?? ex.Message}";
            _logger.LogError(message);
            return StatusCode(500, message);
        }
    }
}