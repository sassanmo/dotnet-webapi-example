namespace Todo.Application.Abstractions;

using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Domain;

/// <summary>
/// Out-port: persistence abstraction (DB).
/// </summary>
public interface ITodoRepository
{
    Task<IReadOnlyList<TodoItem>> ListAsync();
    Task<TodoItem?> GetByIdAsync(string id);
    Task<TodoItem> CreateAsync(TodoItem todo);
    Task<bool> UpdateAsync(TodoItem todo);
    Task<bool> DeleteAsync(string id);
}
