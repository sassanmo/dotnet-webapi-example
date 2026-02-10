using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Todo.Application.Abstractions;
using Todo.Domain;

namespace Todo.Infrastructure;

public sealed class MongoTodoRepository : ITodoRepository
{
    private readonly IMongoCollection<TodoItem> _collection;

    public MongoTodoRepository(IMongoClient client)
    {
        var db = client.GetDatabase("todo");
        _collection = db.GetCollection<TodoItem>("todos");
    }

    public async Task<IReadOnlyList<TodoItem>> ListAsync()
    {
        var todos = await _collection.Find(_ => true).ToListAsync();
        return todos;
    }

    public async Task<TodoItem?> GetByIdAsync(string id)
    {
        var filter = Builders<TodoItem>.Filter.Eq(x => x.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<TodoItem> CreateAsync(TodoItem todo)
    {
        await _collection.InsertOneAsync(todo);
        return todo;
    }

    public async Task<bool> UpdateAsync(TodoItem todo)
    {
        var filter = Builders<TodoItem>.Filter.Eq(x => x.Id, todo.Id);
        var result = await _collection.ReplaceOneAsync(filter, todo);
        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var filter = Builders<TodoItem>.Filter.Eq(x => x.Id, id);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
