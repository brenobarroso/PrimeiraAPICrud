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

            var todoDbSetFake = new Mock<DbSet<Todo>>();
            todoDbSetFake.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>()))
                .Callback((CancellationToken token) => { })
                .Returns((CancellationToken token) => Task.FromResult(todos));

            var dbContextFake = new Mock<AppDbContext>();
            dbContextFake.Setup(x => x.Todos).Returns(todoDbSetFake.Object);
            dbContextFake.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Returns(() => Task.FromResult(0));

            var todoController = new TodoController();

            /// Act
            var result = (OkObjectResult)await todoController.GetAsync(dbContextFake.Object);

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}