namespace Todo.Api.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Application.Abstractions;
using Todo.Application.Contracts;

[ApiController]
[Route("api/v1/todos")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoResponse>>> Get()
    {
        var todos = await _todoService.ListAsync();
        return Ok(todos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoResponse>> GetById(string id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        return todo is null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<ActionResult<TodoResponse>> Post([FromBody] CreateTodoRequest request)
    {
        var created = await _todoService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, [FromBody] UpdateTodoRequest updated)
    {
        var updatedOk = await _todoService.UpdateAsync(id, updated);
        return updatedOk ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _todoService.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
