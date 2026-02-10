namespace Todo.Application.Abstractions;

using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.Contracts;

/// <summary>
/// In-port: application use-cases exposed to the web layer.
/// </summary>
public interface ITodoService
{
    Task<IReadOnlyList<TodoResponse>> ListAsync();
    Task<TodoResponse?> GetByIdAsync(string id);
    Task<TodoResponse> CreateAsync(CreateTodoRequest request);
    Task<bool> UpdateAsync(string id, UpdateTodoRequest request);
    Task<bool> DeleteAsync(string id);
}
