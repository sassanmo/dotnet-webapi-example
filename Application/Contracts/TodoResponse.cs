namespace Todo.Application.Contracts;

using System;

public sealed class TodoResponse
{
    public string Id { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? DueDate { get; init; }
    public DateTime UpdateTime { get; init; }
}
