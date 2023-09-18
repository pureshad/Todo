using AutoMapper;
using Todo.Application.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TodoItem, TodoItemDto>();
        CreateMap<CreateTodoItemCommand, TodoItem>();
        CreateMap<UpdateTodoItemCommand, TodoItem>();
        CreateMap<DeleteTodoItemCommand, TodoItem>();
    }
}