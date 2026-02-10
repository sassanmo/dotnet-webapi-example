namespace Todo.Application.Services;

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.Abstractions;
using Todo.Application.Contracts;
using Todo.Domain;

public sealed class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<TodoResponse>> ListAsync()
    {
        var todos = await _repository.ListAsync();
        return _mapper.Map<List<TodoResponse>>(todos);
    }

    public async Task<TodoResponse?> GetByIdAsync(string id)
    {
        var todo = await _repository.GetByIdAsync(id);
        return todo is null ? null : _mapper.Map<TodoResponse>(todo);
    }

    public async Task<TodoResponse> CreateAsync(CreateTodoRequest request)
    {
        var newTodo = _mapper.Map<TodoItem>(request);
        newTodo.UpdateTime = DateTime.UtcNow;

        var created = await _repository.CreateAsync(newTodo);
        return _mapper.Map<TodoResponse>(created);
    }

    public async Task<bool> UpdateAsync(string id, UpdateTodoRequest request)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null) return false;

        _mapper.Map(request, existing);
        existing.UpdateTime = DateTime.UtcNow;

        return await _repository.UpdateAsync(existing);
    }

    public Task<bool> DeleteAsync(string id)
    {
        return _repository.DeleteAsync(id);
    }
}
