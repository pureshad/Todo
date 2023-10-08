public class UpdateTodoItemCommand : CreateTodoItemCommandBase, IRequest
{
    public int Id { get; set; }
    public bool? IsCompleted { get; set; }
}
