namespace Todo.Application.Commands
{
    public class UpdateCompletedTodoItemCommand : IRequest
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
    }
}