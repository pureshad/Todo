using MediatR;

public class DeleteTodoItemCommand : IRequest
{
    public int Id { get; set; }
}
