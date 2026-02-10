using Todo;
using Todo.Application.Abstractions;
using Todo.Application.Mapping;
using Todo.Application.Services;
using Todo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddValidation();

builder.Services.AddAutoMapper(typeof(TodoMappingProfile));

// MongoDB
builder.Services.AddSingleton<MongoDB.Driver.IMongoClient>(sp =>
    new MongoDB.Driver.MongoClient(builder.Configuration.GetSection(ConfigurationKeys.MongoDbConnectionString).Value)
);

// DI
builder.Services.AddSingleton<ITodoRepository, MongoTodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
