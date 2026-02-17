using Xunit;
using Moq;
using Todo.Application.Services;
using Todo.Application.Abstractions;
using Todo.Application.Contracts;
using Todo.Domain;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    public class TodoServiceTests
    {
        private readonly Mock<ITodoRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TodoService _service;

        public TodoServiceTests()
        {
            _mockRepo = new Mock<ITodoRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new TodoService(_mockRepo.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task ListAsync_ReturnsMappedTodoResponses()
        {
            // Arrange
            var todos = new List<TodoItem> { new TodoItem { Id = "1", Title = "Test" } };
            var responses = new List<TodoResponse> { new TodoResponse { Id = "1", Title = "Test" } };
            _mockRepo.Setup(r => r.ListAsync()).ReturnsAsync(todos);
            _mockMapper.Setup(m => m.Map<List<TodoResponse>>(todos)).Returns(responses);

            // Act
            var result = await _service.ListAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Test", result[0].Title);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMappedTodoResponse_WhenFound()
        {
            // Arrange
            var todo = new TodoItem { Id = "1", Title = "Test" };
            var response = new TodoResponse { Id = "1", Title = "Test" };
            _mockRepo.Setup(r => r.GetByIdAsync("1")).ReturnsAsync(todo);
            _mockMapper.Setup(m => m.Map<TodoResponse>(todo)).Returns(response);

            // Act
            var result = await _service.GetByIdAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test", result!.Title);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetByIdAsync("1")).ReturnsAsync((TodoItem?)null);

            // Act
            var result = await _service.GetByIdAsync("1");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_CreatesAndReturnsMappedTodoResponse()
        {
            // Arrange
            var request = new CreateTodoRequest { Title = "New Todo" };
            var todo = new TodoItem { Id = "1", Title = "New Todo" };
            var created = new TodoItem { Id = "1", Title = "New Todo" };
            var response = new TodoResponse { Id = "1", Title = "New Todo" };
            _mockMapper.Setup(m => m.Map<TodoItem>(request)).Returns(todo);
            _mockRepo.Setup(r => r.CreateAsync(It.IsAny<TodoItem>())).ReturnsAsync(created);
            _mockMapper.Setup(m => m.Map<TodoResponse>(created)).Returns(response);

            // Act
            var result = await _service.CreateAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Todo", result.Title);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenTodoExists()
        {
            // Arrange
            var id = "1";
            var request = new UpdateTodoRequest { Title = "Updated" };
            var existing = new TodoItem { Id = id, Title = "Old" };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(existing);
            _mockMapper.Setup(m => m.Map(request, existing));
            _mockRepo.Setup(r => r.UpdateAsync(existing)).ReturnsAsync(true);

            // Act
            var result = await _service.UpdateAsync(id, request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsFalse_WhenTodoDoesNotExist()
        {
            // Arrange
            var id = "1";
            var request = new UpdateTodoRequest { Title = "Updated" };
            _mockRepo.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((TodoItem?)null);

            // Act
            var result = await _service.UpdateAsync(id, request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenRepositoryReturnsTrue()
        {
            // Arrange
            var id = "1";
            _mockRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_WhenRepositoryReturnsFalse()
        {
            // Arrange
            var id = "1";
            _mockRepo.Setup(r => r.DeleteAsync(id)).ReturnsAsync(false);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.False(result);
        }
        
    }
}
