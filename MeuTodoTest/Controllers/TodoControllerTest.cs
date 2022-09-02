using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MeuTodo.Controllers;
using MeuTodo.Data;
using MeuTodo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
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
            var todos = new List<Todo>();
            todos.Add(new Todo
            {
                Id = 1,
                Title = "Todo test #1",
            });
            todos.Add(new Todo
            {
                Id = 2,
                Title = "Todo test #2",
            });
            todos.Add(new Todo
            {
                Id = 3,
                Title = "Todo test #3",
            });

            var todoController = new TodoController();

            /// Act
            var result = (OkObjectResult)await todoController.GetAsync(_context);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}