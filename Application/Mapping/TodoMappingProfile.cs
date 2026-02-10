namespace Todo.Application.Mapping;

using AutoMapper;
using Todo.Application.Contracts;
using Todo.Domain;


public sealed class TodoMappingProfile : Profile
{
    public TodoMappingProfile()
    {
        CreateMap<TodoItem, TodoResponse>();
        CreateMap<CreateTodoRequest, TodoItem>();
        CreateMap<UpdateTodoRequest, TodoItem>();
    }
}
