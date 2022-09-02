using System;
using System.Linq;
using System.Threading.Tasks;
using MeuTodo.Controllers;
using MeuTodo.Data;
using MeuTodo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MeuTodoTest.Controllers
{
    public class TodoControllerTest
    {
        protected readonly AppDbContext _context;
        public TodoControllerTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task ShouldListAll()
        {
            /// Arrange
            var todoController = new TodoController();

            /// Act
            var result = (OkObjectResult)await todoController.GetAsync(_context);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ShouldAddTodo()
        {
            /// Arrange
            var model = new CreateTodoViewModel
            {
                Title = "Todo teste"
            };

            var todoController = new TodoController();

            /// Act
            var result = (CreatedResult)await todoController.PostAsync(_context, model);

            var allTodos = await _context.Todos.ToListAsync();

            // Assert
            Assert.Equal(201, result.StatusCode);
            Assert.NotEmpty(allTodos);
            Assert.Single(allTodos);
            Assert.Equal(model.Title, allTodos.First().Title);
            Assert.False(allTodos.First().Done);
            Assert.Equal(DateTime.Today, allTodos.First().Date.Date);
        }
    }
}