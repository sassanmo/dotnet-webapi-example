namespace Todo.Domain;

using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public sealed class TodoItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;
    [BsonElement("description")]
    public string? Description { get; set; }
    [BsonElement("dueDate")]
    public DateTime? DueDate { get; set; }
    [BsonElement("updateTime")]
    public DateTime UpdateTime { get; set; } = DateTime.UtcNow;
}
