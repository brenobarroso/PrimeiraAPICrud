using Microsoft.AspNetCore.Mvc;
using MeuTodo.Models;
using System.Collections.Generic;
using MeuTodo.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route ("v1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route("todos")]
        public async Task<IActionResult> Get(
            [FromServices] AppDbContext context)
        {
            var todos = await context
            .Todos
            .AsNoTracking()
            .ToListAsync();
            return Ok(todos);
        }
    }
}