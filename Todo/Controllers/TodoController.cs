using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Infrastructure.Models;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
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
            return StatusCode(500, $"An error occurred while creating the todo item. Message {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoItem(int id, UpdateTodoItemCommand command)
    {
        try
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while updating the todo item. Message {ex.InnerException?.Message ?? ex.Message}");
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
            return StatusCode(500, $"An error occurred while deleting the todo item. Message {ex.InnerException?.Message ?? ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItemDto>>> GetTodoItems()
    {
        try
        {
            var query = new GetTodoItemsQuery();
            var items = await _mediator.Send(query);
            return Ok(items);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching todo items. Message {ex.InnerException?.Message ?? ex.Message}");
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
                return NotFound();
            }

            return Ok(item);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while fetching the todo item. Message {ex.InnerException?.Message ?? ex.Message}");
        }
    }
}