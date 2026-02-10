namespace Todo.Application.Contracts;

using System;
using System.ComponentModel.DataAnnotations;

public sealed class CreateTodoRequest
{
    [Required]
    [MinLength(1)]
    public string Title { get; init; } = string.Empty;

    public string? Description { get; init; }

    public DateTime? DueDate { get; init; }
}
